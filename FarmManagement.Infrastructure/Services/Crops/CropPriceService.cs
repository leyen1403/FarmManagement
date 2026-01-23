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
/// Service quản lý giá cây trồng.
/// </summary>
public class CropPriceService : ICropPriceService
{
    private readonly FarmManagementDbContext _context;
    private readonly IMapper _mapper;
    private readonly IActivityLogService _activityLogService;

    public CropPriceService(FarmManagementDbContext context, IMapper mapper, IActivityLogService activityLogService)
    {
        _context = context;
        _mapper = mapper;
        _activityLogService = activityLogService;
    }

    public async Task<IEnumerable<CropPriceDto>> GetAllAsync(int? cropId = null, bool includeInactive = false)
    {
        var query = _context.CropPrices
            .Include(x => x.Crop)
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        if (cropId.HasValue)
        {
            query = query.Where(x => x.CropId == cropId.Value);
        }

        if (!includeInactive)
        {
            query = query.Where(x => x.IsActive);
        }

        var prices = await query.OrderByDescending(x => x.EffectiveDate).ToListAsync();

        return prices.Select(p => new CropPriceDto
        {
            Id = p.Id,
            CropId = p.CropId,
            CropName = p.Crop?.Name,
            Price = p.Price,
            Unit = p.Unit,
            EffectiveDate = p.EffectiveDate,
            ExpiryDate = p.ExpiryDate,
            Note = p.Note,
            IsActive = p.IsActive,
            CreatedDate = p.CreatedDate
        }).ToList();
    }

    public async Task<CropPriceDto?> GetByIdAsync(int id)
    {
        var price = await _context.CropPrices
            .Include(x => x.Crop)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (price == null)
        {
            return null;
        }

        return new CropPriceDto
        {
            Id = price.Id,
            CropId = price.CropId,
            CropName = price.Crop?.Name,
            Price = price.Price,
            Unit = price.Unit,
            EffectiveDate = price.EffectiveDate,
            ExpiryDate = price.ExpiryDate,
            Note = price.Note,
            IsActive = price.IsActive,
            CreatedDate = price.CreatedDate
        };
    }

    public async Task<CropPriceDto?> GetCurrentPriceAsync(int cropId)
    {
        var now = DateTime.UtcNow;
        var price = await _context.CropPrices
            .Include(x => x.Crop)
            .Where(x => x.CropId == cropId
                   && !x.IsDeleted
                   && x.IsActive
                   && x.EffectiveDate <= now
                   && (x.ExpiryDate == null || x.ExpiryDate > now))
            .OrderByDescending(x => x.EffectiveDate)
            .FirstOrDefaultAsync();

        if (price == null)
        {
            return null;
        }

        return new CropPriceDto
        {
            Id = price.Id,
            CropId = price.CropId,
            CropName = price.Crop?.Name,
            Price = price.Price,
            Unit = price.Unit,
            EffectiveDate = price.EffectiveDate,
            ExpiryDate = price.ExpiryDate,
            Note = price.Note,
            IsActive = price.IsActive,
            CreatedDate = price.CreatedDate
        };
    }

    public async Task<int> CreateAsync(CreateCropPriceDto dto)
    {
        // Validate Crop
        var crop = await _context.Crops
            .FirstOrDefaultAsync(x => x.Id == dto.CropId && !x.IsDeleted);

        if (crop == null)
        {
            throw new BusinessException("Cây trồng không tồn tại");
        }

        // Validate dates
        if (dto.ExpiryDate.HasValue && dto.ExpiryDate <= dto.EffectiveDate)
        {
            throw new BusinessException("Ngày hết hạn phải sau ngày áp dụng");
        }

        var entity = new CropPrice
        {
            CropId = dto.CropId,
            Price = dto.Price,
            Unit = dto.Unit,
            EffectiveDate = dto.EffectiveDate,
            ExpiryDate = dto.ExpiryDate,
            Note = dto.Note,
            IsActive = true
        };

        _context.CropPrices.Add(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Create,
            ActivityEntityTypes.CropPrice,
            entity.Id,
            $"{dto.Price:N0} VNĐ/{dto.Unit}",
            $"Thêm giá mới {dto.Price:N0} VNĐ/{dto.Unit} cho cây trồng \"{crop.Name}\""
        );

        return entity.Id;
    }

    public async Task UpdateAsync(int id, UpdateCropPriceDto dto)
    {
        var entity = await _context.CropPrices
            .Include(x => x.Crop)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new NotFoundException("Giá cây trồng không tồn tại");

        // Validate Crop
        bool cropExists = await _context.Crops.AnyAsync(x => x.Id == dto.CropId && !x.IsDeleted);
        if (!cropExists)
        {
            throw new BusinessException("Cây trồng không tồn tại");
        }

        // Validate dates
        if (dto.ExpiryDate.HasValue && dto.ExpiryDate <= dto.EffectiveDate)
        {
            throw new BusinessException("Ngày hết hạn phải sau ngày áp dụng");
        }

        entity.CropId = dto.CropId;
        entity.Price = dto.Price;
        entity.Unit = dto.Unit;
        entity.EffectiveDate = dto.EffectiveDate;
        entity.ExpiryDate = dto.ExpiryDate;
        entity.Note = dto.Note;
        entity.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Update,
            ActivityEntityTypes.CropPrice,
            entity.Id,
            $"{dto.Price:N0} VNĐ/{dto.Unit}",
            $"Cập nhật giá {dto.Price:N0} VNĐ/{dto.Unit} cho cây trồng \"{entity.Crop?.Name}\""
        );
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.CropPrices
            .Include(x => x.Crop)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (entity == null)
        {
            return;
        }

        var priceName = $"{entity.Price:N0} VNĐ/{entity.Unit}";
        var cropName = entity.Crop?.Name ?? "Unknown";

        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Delete,
            ActivityEntityTypes.CropPrice,
            id,
            priceName,
            $"Xóa giá {priceName} của cây trồng \"{cropName}\""
        );
    }
}