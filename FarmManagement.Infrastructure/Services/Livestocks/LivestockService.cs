using AutoMapper;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Exceptions;
using FarmManagement.Application.Interfaces.Livestocks;
using FarmManagement.Application.Interfaces.Common;
using FarmManagement.Domain.Entities.Livestocks;
using FarmManagement.Domain.Entities.Common;
using FarmManagement.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.Infrastructure.Services.Livestocks;

public class LivestockService : ILivestockService
{
    private readonly FarmManagementDbContext _context;
    private readonly IMapper _mapper;
    private readonly IActivityLogService _activityLogService;

    public LivestockService(FarmManagementDbContext context, IMapper mapper, IActivityLogService activityLogService)
    {
        _context = context;
        _mapper = mapper;
        _activityLogService = activityLogService;
    }

    public async Task<IEnumerable<LivestockDto>> GetAllAsync(int? livestockTypeId = null, int? livestockStatusId = null, int? locationId = null)
    {
        var query = _context.Livestocks
         .Include(x => x.LivestockType)
      .Include(x => x.LivestockStatus)
             .Where(x => !x.IsDeleted)
       .AsQueryable();

        if (livestockTypeId.HasValue)
            query = query.Where(x => x.LivestockTypeId == livestockTypeId.Value);

        if (livestockStatusId.HasValue)
            query = query.Where(x => x.LivestockStatusId == livestockStatusId.Value);

        if (locationId.HasValue)
            query = query.Where(x => x.LocationId == locationId.Value);

        var items = await query.OrderByDescending(x => x.CreatedDate).ToListAsync();

        return items.Select(x => new LivestockDto
        {
            Id = x.Id,
            LivestockTypeId = x.LivestockTypeId,
            LivestockTypeName = x.LivestockType?.Name,
            LocationId = x.LocationId,
            LocationName = _context.Locations.FirstOrDefault(l => l.Id == x.LocationId && !l.IsDeleted)?.Name,
            LivestockStatusId = x.LivestockStatusId,
            LivestockStatusName = x.LivestockStatus?.Name,
            TagCode = x.TagCode,
            Name = x.Name,
            Quantity = x.Quantity,
            MaleCount = x.MaleCount,
            FemaleCount = x.FemaleCount,
            ImportDate = x.ImportDate,
            ImportWeight = x.ImportWeight,
            TotalImportWeight = x.TotalImportWeight,
            ImportPrice = x.ImportPrice,
            TotalImportPrice = x.TotalImportPrice,
            Note = x.Note,
            CreatedDate = x.CreatedDate,
            UpdatedDate = x.UpdatedDate
        }).ToList();
    }

    public async Task<LivestockDto?> GetByIdAsync(int id)
    {
        var x = await _context.Livestocks
                   .Include(i => i.LivestockType)
             .Include(i => i.LivestockStatus)
            .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);

        if (x == null)
            return null;

        return new LivestockDto
        {
            Id = x.Id,
            LivestockTypeId = x.LivestockTypeId,
            LivestockTypeName = x.LivestockType?.Name,
            LocationId = x.LocationId,
            LocationName = _context.Locations.FirstOrDefault(l => l.Id == x.LocationId && !l.IsDeleted)?.Name,
            LivestockStatusId = x.LivestockStatusId,
            LivestockStatusName = x.LivestockStatus?.Name,
            TagCode = x.TagCode,
            Name = x.Name,
            Quantity = x.Quantity,
            MaleCount = x.MaleCount,
            FemaleCount = x.FemaleCount,
            ImportDate = x.ImportDate,
            ImportWeight = x.ImportWeight,
            TotalImportWeight = x.TotalImportWeight,
            ImportPrice = x.ImportPrice,
            TotalImportPrice = x.TotalImportPrice,
            Note = x.Note,
            CreatedDate = x.CreatedDate,
            UpdatedDate = x.UpdatedDate
        };
    }

    public async Task<int> CreateAsync(CreateLivestockDto dto)
    {
        if (!await _context.LivestockTypes.AnyAsync(t => t.Id == dto.LivestockTypeId && !t.IsDeleted))
            throw new BusinessException("Loại vật nuôi không tồn tại");

        if (!await _context.LivestockStatuses.AnyAsync(s => s.Id == dto.LivestockStatusId && !s.IsDeleted))
            throw new BusinessException("Trạng thái vật nuôi không tồn tại");

        if (!await _context.Locations.AnyAsync(l => l.Id == dto.LocationId && !l.IsDeleted))
            throw new BusinessException("Vị trí không tồn tại");

        // Validate MaleCount + FemaleCount <= Quantity
        if (dto.MaleCount + dto.FemaleCount > dto.Quantity)
            throw new BusinessException("Tổng số con đực và cái không được vượt quá tổng số lượng");

        var entity = new Livestock
        {
            LivestockTypeId = dto.LivestockTypeId,
            LocationId = dto.LocationId,
            LivestockStatusId = dto.LivestockStatusId,
            TagCode = dto.TagCode,
            Name = dto.Name,
            Quantity = dto.Quantity,
            MaleCount = dto.MaleCount,
            FemaleCount = dto.FemaleCount,
            ImportDate = dto.ImportDate,
            ImportWeight = dto.ImportWeight,
            TotalImportWeight = dto.ImportWeight * dto.Quantity,
            ImportPrice = dto.ImportPrice,
            TotalImportPrice = dto.ImportPrice * dto.Quantity,
            Note = dto.Note
        };

        _context.Livestocks.Add(entity);
        await _context.SaveChangesAsync();

        var displayName = entity.Name ?? entity.TagCode ?? $"Livestock-{entity.Id}";
        _activityLogService.LogActivity(
      ActivityActionTypes.Create,
            nameof(Livestock),
            entity.Id,
 displayName,
   $"Thêm vật nuôi \"{displayName}\" - SL: {entity.Quantity} con"
        );

        return entity.Id;
    }

    public async Task UpdateAsync(int id, UpdateLivestockDto dto)
    {
        var entity = await _context.Livestocks.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new NotFoundException("Vật nuôi không tồn tại");

        if (!await _context.LivestockTypes.AnyAsync(t => t.Id == dto.LivestockTypeId && !t.IsDeleted))
            throw new BusinessException("Loại vật nuôi không tồn tại");

        if (!await _context.LivestockStatuses.AnyAsync(s => s.Id == dto.LivestockStatusId && !s.IsDeleted))
            throw new BusinessException("Trạng thái vật nuôi không tồn tại");

        if (!await _context.Locations.AnyAsync(l => l.Id == dto.LocationId && !l.IsDeleted))
            throw new BusinessException("Vị trí không tồn tại");

        // Validate MaleCount + FemaleCount <= Quantity
        if (dto.MaleCount + dto.FemaleCount > dto.Quantity)
            throw new BusinessException("Tổng số con đực và cái không được vượt quá tổng số lượng");

        entity.LivestockTypeId = dto.LivestockTypeId;
        entity.LocationId = dto.LocationId;
        entity.LivestockStatusId = dto.LivestockStatusId;
        entity.TagCode = dto.TagCode;
        entity.Name = dto.Name;
        entity.Quantity = dto.Quantity;
        entity.MaleCount = dto.MaleCount;
        entity.FemaleCount = dto.FemaleCount;
        entity.ImportDate = dto.ImportDate;
        entity.ImportWeight = dto.ImportWeight;
        entity.TotalImportWeight = dto.ImportWeight * dto.Quantity;
        entity.ImportPrice = dto.ImportPrice;
        entity.TotalImportPrice = dto.ImportPrice * dto.Quantity;
        entity.Note = dto.Note;

        await _context.SaveChangesAsync();

        var displayName = entity.Name ?? entity.TagCode ?? $"Livestock-{entity.Id}";
        _activityLogService.LogActivity(
              ActivityActionTypes.Update,
        nameof(Livestock),
              entity.Id,
      displayName,
     $"Cập nhật vật nuôi \"{displayName}\" - SL: {entity.Quantity} con"
          );
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Livestocks
    .Include(x => x.LivestockCareLogs)
            .Include(x => x.LivestockHealthLogs)
       .Include(x => x.LivestockSales)
    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (entity == null)
            return;

        // Check if there are any related records
        if (entity.LivestockCareLogs.Any())
            throw new BusinessException("Không thể xóa vật nuôi có nhật ký chăm sóc");

        if (entity.LivestockHealthLogs.Any())
            throw new BusinessException("Không thể xóa vật nuôi có nhật ký sức khỏe");

        if (entity.LivestockSales.Any())
            throw new BusinessException("Không thể xóa vật nuôi có giao dịch bán");

        var displayName = entity.Name ?? entity.TagCode ?? $"Livestock-{entity.Id}";

        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
      ActivityActionTypes.Delete,
              nameof(Livestock),
                  id,
            displayName,
                  $"Xóa vật nuôi \"{displayName}\""
              );
    }
}
