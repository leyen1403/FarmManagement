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

public class LivestockTypeService : ILivestockTypeService
{
    private readonly FarmManagementDbContext _context;
    private readonly IMapper _mapper;
    private readonly IActivityLogService _activityLogService;

    public LivestockTypeService(FarmManagementDbContext context, IMapper mapper, IActivityLogService activityLogService)
    {
        _context = context;
        _mapper = mapper;
        _activityLogService = activityLogService;
    }

    public async Task<List<LivestockTypeDto>> GetAllAsync(bool includeInactive = false)
    {
        var query = _context.LivestockTypes.Where(x => !x.IsDeleted);

        var items = await query.OrderBy(x => x.Name).ToListAsync();

        return items.Select(x => new LivestockTypeDto
        {
            Id = x.Id,
            Code = x.Code,
            Name = x.Name,
            Description = x.Description,
            IsActive = true // Default to true since entity doesn't have IsActive
        }).ToList();
    }

    public async Task<LivestockTypeDto?> GetByIdAsync(int id)
    {
        var entity = await _context.LivestockTypes
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (entity == null)
            return null;

        return new LivestockTypeDto
        {
            Id = entity.Id,
            Code = entity.Code,
            Name = entity.Name,
            Description = entity.Description,
            IsActive = true
        };
    }

    public async Task<int> CreateAsync(CreateLivestockTypeDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Code))
            throw new BusinessException("Mã loại vật nuôi không được để trống");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new BusinessException("Tên loại vật nuôi không được để trống");

        if (await _context.LivestockTypes.AnyAsync(x => x.Code == dto.Code && !x.IsDeleted))
            throw new BusinessException("Mã loại vật nuôi đã tồn tại");

        var entity = new LivestockType
        {
            Code = dto.Code,
            Name = dto.Name,
            Description = dto.Description
        };

        _context.LivestockTypes.Add(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Create,
            nameof(LivestockType),
            entity.Id,
            entity.Name,
            $"Thêm loại vật nuôi \"{entity.Name}\""
        );

        return entity.Id;
    }

    public async Task UpdateAsync(int id, UpdateLivestockTypeDto dto)
    {
        var entity = await _context.LivestockTypes
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new NotFoundException("Loại vật nuôi không tồn tại");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new BusinessException("Tên loại vật nuôi không được để trống");

        entity.Name = dto.Name;
        entity.Description = dto.Description;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Update,
            nameof(LivestockType),
            entity.Id,
            entity.Name,
            $"Cập nhật loại vật nuôi \"{entity.Name}\""
        );
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.LivestockTypes
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (entity == null)
            return;

        // Check if any livestock uses this type
        if (await _context.Livestocks.AnyAsync(x => x.LivestockTypeId == id && !x.IsDeleted))
            throw new BusinessException("Không thể xóa loại vật nuôi đang được sử dụng");

        var name = entity.Name;

        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Delete,
            nameof(LivestockType),
            id,
            name,
            $"Xóa loại vật nuôi \"{name}\""
        );
    }
}
