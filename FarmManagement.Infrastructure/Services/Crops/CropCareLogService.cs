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
/// Service quản lý nhật ký chăm sóc cây trồng.
/// </summary>
public class CropCareLogService : ICropCareLogService
{
    private readonly FarmManagementDbContext _context;
    private readonly IMapper _mapper;
    private readonly IActivityLogService _activityLogService;

    public CropCareLogService(FarmManagementDbContext context, IMapper mapper, IActivityLogService activityLogService)
    {
        _context = context;
        _mapper = mapper;
        _activityLogService = activityLogService;
    }

    public async Task<IEnumerable<CropCareLogDto>> GetByCropIdAsync(int cropId)
    {
        var logs = await _context.CropCareLogs
            .Include(x => x.Crop)
            .Include(x => x.CropCareType)
            .Where(x => x.CropId == cropId && !x.IsDeleted)
            .OrderByDescending(x => x.CareDate)
            .ToListAsync();

        return logs.Select(l => new CropCareLogDto
        {
            Id = l.Id,
            CropId = l.CropId,
            CropName = l.Crop?.Name,
            CropCareTypeId = l.CropCareTypeId,
            CropCareTypeName = l.CropCareType?.Name,
            CareDate = l.CareDate,
            Description = l.Description,
            Cost = l.Cost,
            CreatedDate = l.CreatedDate
        }).ToList();
    }

    public async Task<CropCareLogDto?> GetByIdAsync(int id)
    {
        var log = await _context.CropCareLogs
            .Include(x => x.Crop)
            .Include(x => x.CropCareType)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (log == null)
        {
            return null;
        }

        return new CropCareLogDto
        {
            Id = log.Id,
            CropId = log.CropId,
            CropName = log.Crop?.Name,
            CropCareTypeId = log.CropCareTypeId,
            CropCareTypeName = log.CropCareType?.Name,
            CareDate = log.CareDate,
            Description = log.Description,
            Cost = log.Cost,
            CreatedDate = log.CreatedDate
        };
    }

    public async Task<int> CreateAsync(CreateCropCareLogDto dto)
    {
        // Validate Crop
        var crop = await _context.Crops
            .FirstOrDefaultAsync(x => x.Id == dto.CropId && !x.IsDeleted);

        if (crop == null)
        {
            throw new BusinessException("Cây trồng không tồn tại");
        }

        // Validate CropCareType
        var careType = await _context.CropCareTypes
            .FirstOrDefaultAsync(x => x.Id == dto.CropCareTypeId);

        if (careType == null)
        {
            throw new BusinessException("Loại chăm sóc không tồn tại");
        }

        var entity = new CropCareLog
        {
            CropId = dto.CropId,
            CropCareTypeId = dto.CropCareTypeId,
            CareDate = dto.CareDate,
            Description = dto.Description,
            Cost = dto.Cost
        };

        _context.CropCareLogs.Add(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Create,
            ActivityEntityTypes.CropCareLog,
            entity.Id,
            careType.Name,
            $"Thêm nhật ký chăm sóc \"{careType.Name}\" cho cây trồng \"{crop.Name}\""
        );

        return entity.Id;
    }

    public async Task UpdateAsync(int id, UpdateCropCareLogDto dto)
    {
        var entity = await _context.CropCareLogs
            .Include(x => x.Crop)
            .Include(x => x.CropCareType)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new NotFoundException("Nhật ký chăm sóc không tồn tại");

        // Validate Crop
        bool cropExists = await _context.Crops
            .AnyAsync(x => x.Id == dto.CropId && !x.IsDeleted);

        if (!cropExists)
        {
            throw new BusinessException("Cây trồng không tồn tại");
        }

        // Validate CropCareType
        bool careTypeExists = await _context.CropCareTypes
            .AnyAsync(x => x.Id == dto.CropCareTypeId);

        if (!careTypeExists)
        {
            throw new BusinessException("Loại chăm sóc không tồn tại");
        }

        entity.CropId = dto.CropId;
        entity.CropCareTypeId = dto.CropCareTypeId;
        entity.CareDate = dto.CareDate;
        entity.Description = dto.Description;
        entity.Cost = dto.Cost;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Update,
            ActivityEntityTypes.CropCareLog,
            entity.Id,
            entity.CropCareType?.Name ?? "Unknown",
            $"Cập nhật nhật ký chăm sóc của cây trồng \"{entity.Crop?.Name}\""
        );
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.CropCareLogs
            .Include(x => x.Crop)
            .Include(x => x.CropCareType)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (entity == null)
        {
            return;
        }

        var careTypeName = entity.CropCareType?.Name ?? "Unknown";
        var cropName = entity.Crop?.Name ?? "Unknown";

        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Delete,
            ActivityEntityTypes.CropCareLog,
            id,
            careTypeName,
            $"Xóa nhật ký chăm sóc \"{careTypeName}\" của cây trồng \"{cropName}\""
        );
    }
}