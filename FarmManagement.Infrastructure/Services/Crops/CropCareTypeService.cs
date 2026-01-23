using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Exceptions;
using FarmManagement.Application.Interfaces.Crops;
using FarmManagement.Application.Interfaces.Common;
using FarmManagement.Domain.Entities.Crops;
using FarmManagement.Domain.Entities.Common;
using FarmManagement.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.Infrastructure.Services.Crops;

/// <summary>
/// Service quản lý loại chăm sóc cây trồng.
/// </summary>
public class CropCareTypeService : ICropCareTypeService
{
    private readonly FarmManagementDbContext _context;
    private readonly IActivityLogService _activityLogService;

    public CropCareTypeService(FarmManagementDbContext context, IActivityLogService activityLogService)
    {
        _context = context;
        _activityLogService = activityLogService;
    }

    public async Task<IEnumerable<CropCareTypeDto>> GetAllAsync(bool includeInactive = false)
    {
        var query = _context.CropCareTypes.Where(x => !x.IsDeleted);

        if (!includeInactive)
        {
            query = query.Where(x => x.IsActive);
        }

        var careTypes = await query.OrderBy(x => x.Name).ToListAsync();

        return careTypes.Select(ct => new CropCareTypeDto
        {
            Id = ct.Id,
            Code = ct.Code,
            Name = ct.Name,
            Description = ct.Description,
            IsActive = ct.IsActive
        }).ToList();
    }

    public async Task<CropCareTypeDto?> GetByIdAsync(int id)
    {
        var careType = await _context.CropCareTypes
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (careType == null)
        {
            return null;
        }

        return new CropCareTypeDto
        {
            Id = careType.Id,
            Code = careType.Code,
            Name = careType.Name,
            Description = careType.Description,
            IsActive = careType.IsActive
        };
    }

    public async Task<int> CreateAsync(CreateCropCareTypeDto dto)
    {
        // Check duplicate code
        bool exists = await _context.CropCareTypes
            .AnyAsync(x => x.Code == dto.Code && !x.IsDeleted);

        if (exists)
        {
            throw new BusinessException("Mã loại chăm sóc đã tồn tại");
        }

        var entity = new CropCareType
        {
            Code = dto.Code,
            Name = dto.Name,
            Description = dto.Description,
            IsActive = true
        };

        _context.CropCareTypes.Add(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Create,
            ActivityEntityTypes.CropCareType,
            entity.Id,
            entity.Name,
            $"Thêm loại chăm sóc mới \"{entity.Name}\""
        );

        return entity.Id;
    }

    public async Task UpdateAsync(int id, UpdateCropCareTypeDto dto)
    {
        var entity = await _context.CropCareTypes
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new NotFoundException("Loại chăm sóc không tồn tại");

        // Check duplicate code (exclude current)
        bool duplicateCode = await _context.CropCareTypes
            .AnyAsync(x => x.Code == dto.Code && x.Id != id && !x.IsDeleted);

        if (duplicateCode)
        {
            throw new BusinessException("Mã loại chăm sóc đã tồn tại");
        }

        entity.Code = dto.Code;
        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Update,
            ActivityEntityTypes.CropCareType,
            entity.Id,
            entity.Name,
            $"Cập nhật loại chăm sóc \"{entity.Name}\""
        );
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.CropCareTypes
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (entity == null)
        {
            return;
        }

        // Check if has related records
        bool inUse = await _context.CropCareLogs
            .AnyAsync(x => x.CropCareTypeId == id && !x.IsDeleted);

        if (inUse)
        {
            throw new BusinessException("Không thể xóa loại chăm sóc đang được sử dụng");
        }

        var entityName = entity.Name;
        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Delete,
            ActivityEntityTypes.CropCareType,
            id,
            entityName,
            $"Xóa loại chăm sóc \"{entityName}\""
        );
    }
}