using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Exceptions;
using FarmManagement.Application.Interfaces.Livestocks;
using FarmManagement.Application.Interfaces.Common;
using FarmManagement.Domain.Entities.Livestocks;
using FarmManagement.Domain.Entities.Common;
using FarmManagement.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.Infrastructure.Services.Livestocks;

public class LivestockHealthLogService : ILivestockHealthLogService
{
    private readonly FarmManagementDbContext _context;
    private readonly IActivityLogService _activityLogService;

    public LivestockHealthLogService(FarmManagementDbContext context, IActivityLogService activityLogService)
    {
        _context = context;
        _activityLogService = activityLogService;
    }

    public async Task<List<LivestockHealthLogDto>> GetByLivestockIdAsync(int livestockId)
    {
        var items = await _context.LivestockHealthLogs
        .Include(x => x.HealthStatus)
        .Where(x => x.LivestockId == livestockId)
        .OrderByDescending(x => x.CheckDate)
        .ToListAsync();

        return items.Select(x => new LivestockHealthLogDto
        {
            Id = x.Id,
            LivestockId = x.LivestockId,
            HealthStatusId = x.HealthStatusId,
            HealthStatusName = x.HealthStatus?.Name,
            CheckDate = x.CheckDate,
            Symptom = x.Symptom,
            Treatment = x.Treatment,
            MedicineCost = x.MedicineCost,
            VetName = x.VetName
        }).ToList();
    }

    public async Task<LivestockHealthLogDto?> GetByIdAsync(int id)
    {
        var entity = await _context.LivestockHealthLogs
            .Include(x => x.HealthStatus)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return null;

        return new LivestockHealthLogDto
        {
            Id = entity.Id,
            LivestockId = entity.LivestockId,
            HealthStatusId = entity.HealthStatusId,
            HealthStatusName = entity.HealthStatus?.Name,
            CheckDate = entity.CheckDate,
            Symptom = entity.Symptom,
            Treatment = entity.Treatment,
            MedicineCost = entity.MedicineCost,
            VetName = entity.VetName
        };
    }

    public async Task<int> CreateAsync(CreateLivestockHealthLogDto dto)
    {
        if (!await _context.Livestocks.AnyAsync(x => x.Id == dto.LivestockId && !x.IsDeleted))
            throw new BusinessException("Vật nuôi không tồn tại");

        if (!await _context.LivestockHealthStatuses.AnyAsync(x => x.Id == dto.HealthStatusId))
            throw new BusinessException("Trạng thái sức khỏe không tồn tại");

        var entity = new LivestockHealthLog
        {
            LivestockId = dto.LivestockId,
            HealthStatusId = dto.HealthStatusId,
            CheckDate = dto.CheckDate,
            Symptom = dto.Symptom,
            Treatment = dto.Treatment,
            MedicineCost = dto.MedicineCost,
            VetName = dto.VetName
        };

        _context.LivestockHealthLogs.Add(entity);
        await _context.SaveChangesAsync();

        var livestock = await _context.Livestocks.FirstOrDefaultAsync(x => x.Id == dto.LivestockId);
        _activityLogService.LogActivity(
            ActivityActionTypes.Create,
            nameof(LivestockHealthLog),
            entity.Id,
            livestock?.TagCode ?? $"Livestock-{dto.LivestockId}",
            $"Thêm nhật ký sức khỏe cho vật nuôi"
        );

        return entity.Id;
    }

    public async Task UpdateAsync(int id, UpdateLivestockHealthLogDto dto)
    {
        var entity = await _context.LivestockHealthLogs
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException("Nhật ký sức khỏe không tồn tại");

        if (!await _context.LivestockHealthStatuses.AnyAsync(x => x.Id == dto.HealthStatusId))
            throw new BusinessException("Trạng thái sức khỏe không tồn tại");

        entity.HealthStatusId = dto.HealthStatusId;
        entity.CheckDate = dto.CheckDate;
        entity.Symptom = dto.Symptom;
        entity.Treatment = dto.Treatment;
        entity.MedicineCost = dto.MedicineCost;
        entity.VetName = dto.VetName;

        await _context.SaveChangesAsync();

        var livestock = await _context.Livestocks.FirstOrDefaultAsync(x => x.Id == entity.LivestockId);
        _activityLogService.LogActivity(
            ActivityActionTypes.Update,
            nameof(LivestockHealthLog),
            entity.Id,
            livestock?.TagCode ?? $"Livestock-{entity.LivestockId}",
            $"Cập nhật nhật ký sức khỏe"
        );
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.LivestockHealthLogs
           .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return;

        var livestockId = entity.LivestockId;
        var livestock = await _context.Livestocks.FirstOrDefaultAsync(x => x.Id == livestockId);

        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Delete,
            nameof(LivestockHealthLog),
            id,
            livestock?.TagCode ?? $"Livestock-{livestockId}",
            $"Xóa nhật ký sức khỏe"
        );
    }
}
