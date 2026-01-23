using AutoMapper;
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
/// Service quản lý chi phí cây trồng.
/// </summary>
public class CropCostService : ICropCostService
{
    private readonly FarmManagementDbContext _context;
    private readonly IMapper _mapper;
    private readonly IActivityLogService _activityLogService;

    public CropCostService(FarmManagementDbContext context, IMapper mapper, IActivityLogService activityLogService)
    {
        _context = context;
        _mapper = mapper;
        _activityLogService = activityLogService;
    }

    public async Task<IEnumerable<CropCostDto>> GetByCropIdAsync(int cropId)
    {
        var costs = await _context.CropCosts
            .Include(x => x.Crop)
            .Include(x => x.CostType)
            .Where(x => x.CropId == cropId && !x.IsDeleted)
            .OrderByDescending(x => x.CostDate)
            .ToListAsync();

        return costs.Select(c => new CropCostDto
        {
            Id = c.Id,
            CropId = c.CropId,
            CropName = c.Crop?.Name,
            CostTypeId = c.CostTypeId,
            CostTypeName = c.CostType?.Name,
            CostDate = c.CostDate,
            Quantity = c.Quantity,
            Unit = c.Unit,
            UnitPrice = c.UnitPrice,
            TotalAmount = c.TotalAmount,
            Note = c.Note,
            CreatedDate = c.CreatedDate
        }).ToList();
    }

    public async Task<CropCostDto?> GetByIdAsync(int id)
    {
        var cost = await _context.CropCosts
            .Include(x => x.Crop)
            .Include(x => x.CostType)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (cost == null)
        {
            return null;
        }

        return new CropCostDto
        {
            Id = cost.Id,
            CropId = cost.CropId,
            CropName = cost.Crop?.Name,
            CostTypeId = cost.CostTypeId,
            CostTypeName = cost.CostType?.Name,
            CostDate = cost.CostDate,
            Quantity = cost.Quantity,
            Unit = cost.Unit,
            UnitPrice = cost.UnitPrice,
            TotalAmount = cost.TotalAmount,
            Note = cost.Note,
            CreatedDate = cost.CreatedDate
        };
    }

    public async Task<int> CreateAsync(CreateCropCostDto dto)
    {
        // Validate Crop
        var crop = await _context.Crops
            .FirstOrDefaultAsync(x => x.Id == dto.CropId && !x.IsDeleted);

        if (crop == null)
        {
            throw new BusinessException("Cây trồng không tồn tại");
        }

        // Validate CostType
        var costType = await _context.CostTypes
            .FirstOrDefaultAsync(x => x.Id == dto.CostTypeId && !x.IsDeleted);

        if (costType == null)
        {
            throw new BusinessException("Loại chi phí không tồn tại");
        }

        var entity = new CropCost
        {
            CropId = dto.CropId,
            CostTypeId = dto.CostTypeId,
            CostDate = dto.CostDate,
            Quantity = dto.Quantity,
            Unit = dto.Unit,
            UnitPrice = dto.UnitPrice,
            TotalAmount = dto.Quantity * dto.UnitPrice,
            Note = dto.Note
        };

        _context.CropCosts.Add(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Create,
            ActivityEntityTypes.CropCost,
            entity.Id,
            $"{costType.Name} - {entity.TotalAmount:N0} VNĐ",
            $"Thêm chi phí {costType.Name} ({entity.TotalAmount:N0} VNĐ) cho cây trồng \"{crop.Name}\""
        );

        return entity.Id;
    }

    public async Task UpdateAsync(int id, UpdateCropCostDto dto)
    {
        var entity = await _context.CropCosts
            .Include(x => x.Crop)
            .Include(x => x.CostType)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new NotFoundException("Chi phí không tồn tại");

        // Validate Crop
        bool cropExists = await _context.Crops
            .AnyAsync(x => x.Id == dto.CropId && !x.IsDeleted);

        if (!cropExists)
        {
            throw new BusinessException("Cây trồng không tồn tại");
        }

        // Validate CostType
        bool costTypeExists = await _context.CostTypes
            .AnyAsync(x => x.Id == dto.CostTypeId && !x.IsDeleted);

        if (!costTypeExists)
        {
            throw new BusinessException("Loại chi phí không tồn tại");
        }

        entity.CropId = dto.CropId;
        entity.CostTypeId = dto.CostTypeId;
        entity.CostDate = dto.CostDate;
        entity.Quantity = dto.Quantity;
        entity.Unit = dto.Unit;
        entity.UnitPrice = dto.UnitPrice;
        entity.TotalAmount = dto.Quantity * dto.UnitPrice;
        entity.Note = dto.Note;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Update,
            ActivityEntityTypes.CropCost,
            entity.Id,
            $"{entity.CostType?.Name} - {entity.TotalAmount:N0} VNĐ",
            $"Cập nhật chi phí của cây trồng \"{entity.Crop?.Name}\""
        );
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.CropCosts
            .Include(x => x.Crop)
            .Include(x => x.CostType)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (entity == null)
        {
            return;
        }

        var costInfo = $"{entity.CostType?.Name} - {entity.TotalAmount:N0} VNĐ";
        var cropName = entity.Crop?.Name ?? "Unknown";

        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Delete,
            ActivityEntityTypes.CropCost,
            id,
            costInfo,
            $"Xóa chi phí {costInfo} của cây trồng \"{cropName}\""
        );
    }
}