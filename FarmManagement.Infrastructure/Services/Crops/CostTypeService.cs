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
/// Service quản lý loại chi phí.
/// </summary>
public class CostTypeService : ICostTypeService
{
    private readonly FarmManagementDbContext _context;
    private readonly IActivityLogService _activityLogService;

    public CostTypeService(FarmManagementDbContext context, IActivityLogService activityLogService)
    {
        _context = context;
        _activityLogService = activityLogService;
    }

    public async Task<IEnumerable<CostTypeDto>> GetAllAsync(bool includeInactive = false)
    {
        var query = _context.CostTypes.Where(x => !x.IsDeleted);

        if (!includeInactive)
        {
            query = query.Where(x => x.IsActive);
        }

        var costTypes = await query.OrderBy(x => x.Name).ToListAsync();

        return costTypes.Select(ct => new CostTypeDto
        {
            Id = ct.Id,
            Code = ct.Code,
            Name = ct.Name,
            Description = ct.Description,
            IsActive = ct.IsActive
        }).ToList();
    }

    public async Task<CostTypeDto?> GetByIdAsync(int id)
    {
        var costType = await _context.CostTypes
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (costType == null)
        {
            return null;
        }

        return new CostTypeDto
        {
            Id = costType.Id,
            Code = costType.Code,
            Name = costType.Name,
            Description = costType.Description,
            IsActive = costType.IsActive
        };
    }

    public async Task<int> CreateAsync(CreateCostTypeDto dto)
    {
        // Check duplicate code
        bool exists = await _context.CostTypes
            .AnyAsync(x => x.Code == dto.Code && !x.IsDeleted);

        if (exists)
        {
            throw new BusinessException("Mã loại chi phí đã tồn tại");
        }

        var entity = new CostType
        {
            Code = dto.Code,
            Name = dto.Name,
            Description = dto.Description,
            IsActive = true
        };

        _context.CostTypes.Add(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Create,
            ActivityEntityTypes.CostType,
            entity.Id,
            entity.Name,
            $"Thêm loại chi phí mới \"{entity.Name}\""
        );

        return entity.Id;
    }

    public async Task UpdateAsync(int id, UpdateCostTypeDto dto)
    {
        var entity = await _context.CostTypes
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new NotFoundException("Loại chi phí không tồn tại");

        // Check duplicate code (exclude current)
        bool duplicateCode = await _context.CostTypes
            .AnyAsync(x => x.Code == dto.Code && x.Id != id && !x.IsDeleted);

        if (duplicateCode)
        {
            throw new BusinessException("Mã loại chi phí đã tồn tại");
        }

        entity.Code = dto.Code;
        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Update,
            ActivityEntityTypes.CostType,
            entity.Id,
            entity.Name,
            $"Cập nhật loại chi phí \"{entity.Name}\""
        );
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.CostTypes
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (entity == null)
        {
            return;
        }

        // Check if has related records
        bool inUse = await _context.CropCosts
            .AnyAsync(x => x.CostTypeId == id && !x.IsDeleted);

        if (inUse)
        {
            throw new BusinessException("Không thể xóa loại chi phí đang được sử dụng");
        }

        var entityName = entity.Name;
        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Delete,
            ActivityEntityTypes.CostType,
            id,
            entityName,
            $"Xóa loại chi phí \"{entityName}\""
        );
    }
}