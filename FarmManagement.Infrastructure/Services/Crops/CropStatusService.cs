using AutoMapper;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;
using FarmManagement.Application.Interfaces.Common;
using FarmManagement.Domain.Entities.Crops;
using FarmManagement.Domain.Entities.Common;
using FarmManagement.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.Infrastructure.Services.Crops;

/// <summary>
/// Service quản lý trạng thái cây trồng.
/// </summary>
public class CropStatusService : ICropStatusService
{
    private readonly FarmManagementDbContext _context;
    private readonly IMapper _mapper;
    private readonly IActivityLogService _activityLogService;

    public CropStatusService(FarmManagementDbContext context, IMapper mapper, IActivityLogService activityLogService)
    {
        _context = context;
        _mapper = mapper;
        _activityLogService = activityLogService;
    }

    public async Task<IEnumerable<CropStatusDto>> GetAllAsync(bool includeInactive)
    {
        var query = _context.CropStatuses
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        if (!includeInactive)
        {
            query = query.Where(x => x.IsActive);
        }

        var cropStatuses = await query.OrderBy(x => x.Name).ToListAsync();
        return _mapper.Map<IEnumerable<CropStatusDto>>(cropStatuses);
    }

    public async Task<CropStatusDto> GetByIdAsync(int id)
    {
        var entity = await _context.CropStatuses
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new KeyNotFoundException($"CropStatus {id} không tồn tại");

        return _mapper.Map<CropStatusDto>(entity);
    }

    public async Task CreateAsync(CreateCropStatusDto dto)
    {
        var code = dto.Code.Trim().ToUpper();

        bool exists = await _context.CropStatuses
            .AnyAsync(x => x.Code.ToUpper() == code && !x.IsDeleted);

        if (exists)
        {
            throw new InvalidOperationException("Mã trạng thái đã tồn tại");
        }

        var entity = _mapper.Map<CropStatus>(dto);
        entity.Code = code;
        entity.IsActive = true;

        _context.CropStatuses.Add(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Create,
            ActivityEntityTypes.CropStatus,
            entity.Id,
            entity.Name,
            $"Thêm trạng thái cây trồng mới \"{entity.Name}\""
        );
    }

    public async Task UpdateAsync(int id, UpdateCropStatusDto dto)
    {
        var entity = await _context.CropStatuses
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new KeyNotFoundException($"CropStatus {id} không tồn tại");

        var code = dto.Code.Trim().ToUpper();

        bool duplicateCode = await _context.CropStatuses
            .AnyAsync(x => x.Id != id && x.Code.ToUpper() == code && !x.IsDeleted);

        if (duplicateCode)
        {
            throw new InvalidOperationException("Mã trạng thái đã tồn tại");
        }

        // Cập nhật thủ công hoặc sử dụng _mapper.Map(dto, entity)
        entity.Code = code;
        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Update,
            ActivityEntityTypes.CropStatus,
            entity.Id,
            entity.Name,
            $"Cập nhật trạng thái cây trồng \"{entity.Name}\""
        );
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.CropStatuses
            .Include(x => x.Crops)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new KeyNotFoundException($"CropStatus {id} không tồn tại");

        if (entity.Crops.Any(x => !x.IsDeleted))
        {
            throw new InvalidOperationException("Không thể xóa trạng thái đang có cây trồng liên kết");
        }

        var entityName = entity.Name;
        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Delete,
            ActivityEntityTypes.CropStatus,
            id,
            entityName,
            $"Xóa trạng thái cây trồng \"{entityName}\""
        );
    }
}