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

public class LivestockStatusService : ILivestockStatusService
{
    private readonly FarmManagementDbContext _context;
    private readonly IMapper _mapper;
    private readonly IActivityLogService _activityLogService;

    public LivestockStatusService(FarmManagementDbContext context, IMapper mapper, IActivityLogService activityLogService)
    {
        _context = context;
        _mapper = mapper;
        _activityLogService = activityLogService;
    }

    public async Task<List<LivestockStatusDto>> GetAllAsync(bool includeInactive = false)
    {
        var query = _context.LivestockStatuses.Where(x => !x.IsDeleted);

        var items = await query.OrderBy(x => x.Name).ToListAsync();

        return items.Select(x => new LivestockStatusDto
        {
            Id = x.Id,
            Code = x.Code,
            Name = x.Name,
            Description = null, // LivestockStatus doesn't have Description
            IsActive = true // Default to true
        }).ToList();
    }

    public async Task<LivestockStatusDto?> GetByIdAsync(int id)
    {
        var entity = await _context.LivestockStatuses
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (entity == null)
            return null;

        return new LivestockStatusDto
        {
            Id = entity.Id,
            Code = entity.Code,
            Name = entity.Name,
            Description = null,
            IsActive = true
        };
    }

    public async Task<int> CreateAsync(CreateLivestockStatusDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Code))
            throw new BusinessException("Mã trạng thái không được để trống");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new BusinessException("Tên trạng thái không được để trống");

        if (await _context.LivestockStatuses.AnyAsync(x => x.Code == dto.Code && !x.IsDeleted))
            throw new BusinessException("Mã trạng thái đã tồn tại");

        var entity = new LivestockStatus
        {
            Code = dto.Code,
            Name = dto.Name
        };

        _context.LivestockStatuses.Add(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Create,
            nameof(LivestockStatus),
            entity.Id,
            entity.Name,
            $"Thêm trạng thái vật nuôi \"{entity.Name}\""
        );

        return entity.Id;
    }

    public async Task UpdateAsync(int id, UpdateLivestockStatusDto dto)
    {
        var entity = await _context.LivestockStatuses
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new NotFoundException("Trạng thái vật nuôi không tồn tại");

        if (string.IsNullOrWhiteSpace(dto.Code))
            throw new BusinessException("Mã trạng thái không được để trống");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new BusinessException("Tên trạng thái không được để trống");

        // Check duplicate code (excluding current)
        if (await _context.LivestockStatuses.AnyAsync(x => x.Code == dto.Code && x.Id != id && !x.IsDeleted))
            throw new BusinessException("Mã trạng thái đã tồn tại");

        entity.Code = dto.Code;
        entity.Name = dto.Name;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Update,
            nameof(LivestockStatus),
            entity.Id,
            entity.Name,
            $"Cập nhật trạng thái vật nuôi \"{entity.Name}\""
        );
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.LivestockStatuses
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (entity == null)
            return;

        if (await _context.Livestocks.AnyAsync(x => x.LivestockStatusId == id && !x.IsDeleted))
            throw new BusinessException("Không thể xóa trạng thái đang được sử dụng");

        var name = entity.Name;

        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Delete,
            nameof(LivestockStatus),
            id,
            name,
            $"Xóa trạng thái vật nuôi \"{name}\""
        );
    }
}
