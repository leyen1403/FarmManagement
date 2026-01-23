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
/// Service quản lý cây trồng.
/// </summary>
public class CropService : ICropService
{
    private readonly FarmManagementDbContext _context;
    private readonly IMapper _mapper;
    private readonly IActivityLogService _activityLogService;

    public CropService(FarmManagementDbContext context, IMapper mapper, IActivityLogService activityLogService)
    {
        _context = context;
        _mapper = mapper;
        _activityLogService = activityLogService;
    }

    public async Task<IEnumerable<CropDto>> GetAllAsync(int? cropTypeId = null, int? cropStatusId = null, int? locationId = null)
    {
        var query = _context.Crops
            .Include(x => x.CropType)
            .Include(x => x.CropStatus)
            .Include(x => x.Location)
            .Include(x => x.CropPrices)
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        if (cropTypeId.HasValue)
        {
            query = query.Where(x => x.CropTypeId == cropTypeId.Value);
        }

        if (cropStatusId.HasValue)
        {
            query = query.Where(x => x.CropStatusId == cropStatusId.Value);
        }

        if (locationId.HasValue)
        {
            query = query.Where(x => x.LocationId == locationId.Value);
        }

        var crops = await query.OrderByDescending(x => x.CreatedDate).ToListAsync();
        var now = DateTime.UtcNow;

        return crops.Select(crop => new CropDto
        {
            Id = crop.Id,
            Name = crop.Name,
            CropTypeId = crop.CropTypeId,
            CropTypeName = crop.CropType?.Name,
            LocationId = crop.LocationId,
            LocationName = crop.Location?.Name,
            CropStatusId = crop.CropStatusId,
            CropStatusName = crop.CropStatus?.Name,
            PlantDate = crop.PlantDate,
            ExpectedHarvestDate = crop.ExpectedHarvestDate,
            ActualHarvestDate = crop.ActualHarvestDate,
            EstimatedYield = crop.EstimatedYield,
            Unit = crop.Unit,
            Note = crop.Note,
            CurrentPrice = crop.CropPrices?
                .Where(p => p.IsActive && !p.IsDeleted && p.EffectiveDate <= now && (p.ExpiryDate == null || p.ExpiryDate > now))
                .OrderByDescending(p => p.EffectiveDate)
                .FirstOrDefault()?.Price,
            CreatedDate = crop.CreatedDate,
            UpdatedDate = crop.UpdatedDate
        }).ToList();
    }

    public async Task<CropDto?> GetByIdAsync(int id)
    {
        var crop = await _context.Crops
            .Include(x => x.CropType)
            .Include(x => x.CropStatus)
            .Include(x => x.Location)
            .Include(x => x.CropPrices)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (crop == null)
        {
            return null;
        }

        var now = DateTime.UtcNow;

        return new CropDto
        {
            Id = crop.Id,
            Name = crop.Name,
            CropTypeId = crop.CropTypeId,
            CropTypeName = crop.CropType?.Name,
            LocationId = crop.LocationId,
            LocationName = crop.Location?.Name,
            CropStatusId = crop.CropStatusId,
            CropStatusName = crop.CropStatus?.Name,
            PlantDate = crop.PlantDate,
            ExpectedHarvestDate = crop.ExpectedHarvestDate,
            ActualHarvestDate = crop.ActualHarvestDate,
            EstimatedYield = crop.EstimatedYield,
            Unit = crop.Unit,
            Note = crop.Note,
            CurrentPrice = crop.CropPrices?
                .Where(p => p.IsActive && !p.IsDeleted && p.EffectiveDate <= now && (p.ExpiryDate == null || p.ExpiryDate > now))
                .OrderByDescending(p => p.EffectiveDate)
                .FirstOrDefault()?.Price,
            CreatedDate = crop.CreatedDate,
            UpdatedDate = crop.UpdatedDate
        };
    }

    public async Task<int> CreateAsync(CreateCropDto dto)
    {
        // Validation logic
        if (!await _context.CropTypes.AnyAsync(x => x.Id == dto.CropTypeId && !x.IsDeleted))
            throw new BusinessException("Loại cây trồng không tồn tại");

        if (!await _context.CropStatuses.AnyAsync(x => x.Id == dto.CropStatusId && !x.IsDeleted))
            throw new BusinessException("Trạng thái cây trồng không tồn tại");

        if (!await _context.Locations.AnyAsync(x => x.Id == dto.LocationId && !x.IsDeleted))
            throw new BusinessException("Vị trí không tồn tại");

        if (dto.ExpectedHarvestDate.HasValue && dto.ExpectedHarvestDate < dto.PlantDate)
            throw new BusinessException("Ngày dự kiến thu hoạch không thể trước ngày trồng");

        var entity = new Crop
        {
            Name = dto.Name,
            CropTypeId = dto.CropTypeId,
            LocationId = dto.LocationId,
            CropStatusId = dto.CropStatusId,
            PlantDate = dto.PlantDate,
            ExpectedHarvestDate = dto.ExpectedHarvestDate,
            EstimatedYield = dto.EstimatedYield,
            Unit = dto.Unit,
            Note = dto.Note
        };

        _context.Crops.Add(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Create,
            ActivityEntityTypes.Crop,
            entity.Id,
            entity.Name,
            $"Thêm cây trồng mới \"{entity.Name}\""
        );

        return entity.Id;
    }

    public async Task UpdateAsync(int id, UpdateCropDto dto)
    {
        var entity = await _context.Crops
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new NotFoundException("Cây trồng không tồn tại");

        // Validation logic
        if (!await _context.CropTypes.AnyAsync(x => x.Id == dto.CropTypeId && !x.IsDeleted))
            throw new BusinessException("Loại cây trồng không tồn tại");

        if (!await _context.CropStatuses.AnyAsync(x => x.Id == dto.CropStatusId && !x.IsDeleted))
            throw new BusinessException("Trạng thái cây trồng không tồn tại");

        if (!await _context.Locations.AnyAsync(x => x.Id == dto.LocationId && !x.IsDeleted))
            throw new BusinessException("Vị trí không tồn tại");

        if (dto.ExpectedHarvestDate.HasValue && dto.ExpectedHarvestDate < dto.PlantDate)
            throw new BusinessException("Ngày dự kiến thu hoạch không thể trước ngày trồng");

        if (dto.ActualHarvestDate.HasValue && dto.ActualHarvestDate < dto.PlantDate)
            throw new BusinessException("Ngày thu hoạch thực tế không thể trước ngày trồng");

        entity.Name = dto.Name;
        entity.CropTypeId = dto.CropTypeId;
        entity.LocationId = dto.LocationId;
        entity.CropStatusId = dto.CropStatusId;
        entity.PlantDate = dto.PlantDate;
        entity.ExpectedHarvestDate = dto.ExpectedHarvestDate;
        entity.ActualHarvestDate = dto.ActualHarvestDate;
        entity.EstimatedYield = dto.EstimatedYield;
        entity.Unit = dto.Unit;
        entity.Note = dto.Note;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Update,
            ActivityEntityTypes.Crop,
            entity.Id,
            entity.Name,
            $"Cập nhật cây trồng \"{entity.Name}\""
        );
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Crops
            .Include(x => x.CropCareLogs)
            .Include(x => x.CropHarvests)
            .Include(x => x.CropCosts)
            .Include(x => x.CropPrices)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (entity == null)
            return;

        // Check dependencies
        if (entity.CropCareLogs.Any(x => !x.IsDeleted))
            throw new BusinessException("Không thể xóa cây trồng có nhật ký chăm sóc");

        if (entity.CropHarvests.Any(x => !x.IsDeleted))
            throw new BusinessException("Không thể xóa cây trồng có thông tin thu hoạch");

        if (entity.CropCosts.Any(x => !x.IsDeleted))
            throw new BusinessException("Không thể xóa cây trồng có chi phí liên quan");

        var cropName = entity.Name;
        var now = DateTime.UtcNow;

        entity.IsDeleted = true;
        entity.DeletedDate = now;

        foreach (var price in entity.CropPrices.Where(x => !x.IsDeleted))
        {
            price.IsDeleted = true;
            price.DeletedDate = now;
        }

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Delete,
            ActivityEntityTypes.Crop,
            id,
            cropName,
            $"Xóa cây trồng \"{cropName}\""
        );
    }
}