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
/// Service quản lý thu hoạch cây trồng.
/// </summary>
public class CropHarvestService : ICropHarvestService
{
    private readonly FarmManagementDbContext _context;
    private readonly IMapper _mapper;
    private readonly IActivityLogService _activityLogService;

    public CropHarvestService(FarmManagementDbContext context, IMapper mapper, IActivityLogService activityLogService)
    {
        _context = context;
        _mapper = mapper;
        _activityLogService = activityLogService;
    }

    public async Task<IEnumerable<CropHarvestDto>> GetByCropIdAsync(int cropId)
    {
        var harvests = await _context.CropHarvests
            .Include(x => x.Crop)
            .Where(x => x.CropId == cropId && !x.IsDeleted)
            .OrderByDescending(x => x.HarvestDate)
            .ToListAsync();

        return harvests.Select(h => new CropHarvestDto
        {
            Id = h.Id,
            CropId = h.CropId,
            CropName = h.Crop?.Name,
            HarvestDate = h.HarvestDate,
            Quantity = h.Quantity,
            Unit = h.Unit,
            UnitPrice = h.UnitPrice,
            TotalAmount = h.TotalAmount,
            Buyer = h.Buyer,
            Note = h.Note,
            CreatedDate = h.CreatedDate
        }).ToList();
    }

    public async Task<CropHarvestDto?> GetByIdAsync(int id)
    {
        var harvest = await _context.CropHarvests
            .Include(x => x.Crop)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (harvest == null)
        {
            return null;
        }

        return new CropHarvestDto
        {
            Id = harvest.Id,
            CropId = harvest.CropId,
            CropName = harvest.Crop?.Name,
            HarvestDate = harvest.HarvestDate,
            Quantity = harvest.Quantity,
            Unit = harvest.Unit,
            UnitPrice = harvest.UnitPrice,
            TotalAmount = harvest.TotalAmount,
            Buyer = harvest.Buyer,
            Note = harvest.Note,
            CreatedDate = harvest.CreatedDate
        };
    }

    public async Task<int> CreateAsync(CreateCropHarvestDto dto)
    {
        // Validate Crop
        var crop = await _context.Crops
            .FirstOrDefaultAsync(x => x.Id == dto.CropId && !x.IsDeleted);

        if (crop == null)
        {
            throw new BusinessException("Cây trồng không tồn tại");
        }

        var entity = new CropHarvest
        {
            CropId = dto.CropId,
            HarvestDate = dto.HarvestDate,
            Quantity = dto.Quantity,
            Unit = dto.Unit,
            UnitPrice = dto.UnitPrice,
            TotalAmount = dto.Quantity * dto.UnitPrice,
            Buyer = dto.Buyer,
            Note = dto.Note
        };

        _context.CropHarvests.Add(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Create,
            ActivityEntityTypes.CropHarvest,
            entity.Id,
            $"{dto.Quantity} {dto.Unit}",
            $"Thêm thu hoạch {dto.Quantity} {dto.Unit} cho cây trồng \"{crop.Name}\""
        );

        return entity.Id;
    }

    public async Task UpdateAsync(int id, UpdateCropHarvestDto dto)
    {
        var entity = await _context.CropHarvests
            .Include(x => x.Crop)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new NotFoundException("Thu hoạch không tồn tại");

        // Validate Crop
        bool cropExists = await _context.Crops
            .AnyAsync(x => x.Id == dto.CropId && !x.IsDeleted);

        if (!cropExists)
        {
            throw new BusinessException("Cây trồng không tồn tại");
        }

        entity.CropId = dto.CropId;
        entity.HarvestDate = dto.HarvestDate;
        entity.Quantity = dto.Quantity;
        entity.Unit = dto.Unit;
        entity.UnitPrice = dto.UnitPrice;
        entity.TotalAmount = dto.Quantity * dto.UnitPrice;
        entity.Buyer = dto.Buyer;
        entity.Note = dto.Note;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Update,
            ActivityEntityTypes.CropHarvest,
            entity.Id,
            $"{dto.Quantity} {dto.Unit}",
            $"Cập nhật thu hoạch của cây trồng \"{entity.Crop?.Name}\""
        );
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.CropHarvests
            .Include(x => x.Crop)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (entity == null)
        {
            return;
        }

        var harvestInfo = $"{entity.Quantity} {entity.Unit}";
        var cropName = entity.Crop?.Name ?? "Unknown";

        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Delete,
            ActivityEntityTypes.CropHarvest,
            id,
            harvestInfo,
            $"Xóa thu hoạch {harvestInfo} của cây trồng \"{cropName}\""
        );
    }
}