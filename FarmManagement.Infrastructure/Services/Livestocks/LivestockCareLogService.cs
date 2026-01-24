using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Exceptions;
using FarmManagement.Application.Interfaces.Livestocks;
using FarmManagement.Application.Interfaces.Common;
using FarmManagement.Domain.Entities.Livestocks;
using FarmManagement.Domain.Entities.Common;
using FarmManagement.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.Infrastructure.Services.Livestocks;

public class LivestockCareLogService : ILivestockCareLogService
{
    private readonly FarmManagementDbContext _context;
    private readonly IActivityLogService _activityLogService;

    public LivestockCareLogService(FarmManagementDbContext context, IActivityLogService activityLogService)
    {
        _context = context;
        _activityLogService = activityLogService;
    }

    public async Task<List<LivestockCareLogDto>> GetByLivestockIdAsync(int livestockId)
    {
        var items = await _context.LivestockCareLogs
     .Include(x => x.LivestockCareType)
   .Where(x => x.LivestockId == livestockId)
     .OrderByDescending(x => x.CareDate)
              .ToListAsync();

        return items.Select(x => new LivestockCareLogDto
        {
            Id = x.Id,
            LivestockId = x.LivestockId,
            LivestockCareTypeId = x.LivestockCareTypeId,
            LivestockCareTypeName = x.LivestockCareType?.Name,
            CareDate = x.CareDate,
            Quantity = x.Quantity,
            Unit = x.Unit,
            Cost = x.Cost,
            Note = x.Note
        }).ToList();
    }

    public async Task<LivestockCareLogDto?> GetByIdAsync(int id)
    {
        var entity = await _context.LivestockCareLogs
          .Include(x => x.LivestockCareType)
        .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return null;

        return new LivestockCareLogDto
        {
            Id = entity.Id,
            LivestockId = entity.LivestockId,
            LivestockCareTypeId = entity.LivestockCareTypeId,
            LivestockCareTypeName = entity.LivestockCareType?.Name,
            CareDate = entity.CareDate,
            Quantity = entity.Quantity,
            Unit = entity.Unit,
            Cost = entity.Cost,
            Note = entity.Note
        };
    }

    public async Task<int> CreateAsync(CreateLivestockCareLogDto dto)
    {
        if (!await _context.Livestocks.AnyAsync(x => x.Id == dto.LivestockId && !x.IsDeleted))
            throw new BusinessException("Vật nuôi không tồn tại");

        if (!await _context.LivestockCareTypes.AnyAsync(x => x.Id == dto.LivestockCareTypeId))
            throw new BusinessException("Loại chăm sóc không tồn tại");

        var entity = new LivestockCareLog
        {
            LivestockId = dto.LivestockId,
            LivestockCareTypeId = dto.LivestockCareTypeId,
            CareDate = dto.CareDate,
            Quantity = dto.Quantity,
            Unit = dto.Unit,
            Cost = dto.Cost,
            Note = dto.Note
        };

        _context.LivestockCareLogs.Add(entity);
        await _context.SaveChangesAsync();

        var livestock = await _context.Livestocks.FirstOrDefaultAsync(x => x.Id == dto.LivestockId);
        _activityLogService.LogActivity(
     ActivityActionTypes.Create,
     nameof(LivestockCareLog),
    entity.Id,
          livestock?.TagCode ?? $"Livestock-{dto.LivestockId}",
          $"Thêm nhật ký chăm sóc cho vật nuôi"
           );

        return entity.Id;
    }

    public async Task UpdateAsync(int id, UpdateLivestockCareLogDto dto)
    {
        var entity = await _context.LivestockCareLogs
    .FirstOrDefaultAsync(x => x.Id == id)
    ?? throw new NotFoundException("Nhật ký chăm sóc không tồn tại");

        if (!await _context.LivestockCareTypes.AnyAsync(x => x.Id == dto.LivestockCareTypeId))
            throw new BusinessException("Loại chăm sóc không tồn tại");

        entity.LivestockCareTypeId = dto.LivestockCareTypeId;
        entity.CareDate = dto.CareDate;
        entity.Quantity = dto.Quantity;
        entity.Unit = dto.Unit;
        entity.Cost = dto.Cost;
        entity.Note = dto.Note;

        await _context.SaveChangesAsync();

        var livestock = await _context.Livestocks.FirstOrDefaultAsync(x => x.Id == entity.LivestockId);
        _activityLogService.LogActivity(
       ActivityActionTypes.Update,
       nameof(LivestockCareLog),
               entity.Id,
      livestock?.TagCode ?? $"Livestock-{entity.LivestockId}",
      $"Cập nhật nhật ký chăm sóc"
           );
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.LivestockCareLogs
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return;

        var livestockId = entity.LivestockId;
        var livestock = await _context.Livestocks.FirstOrDefaultAsync(x => x.Id == livestockId);

        _context.LivestockCareLogs.Remove(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
     ActivityActionTypes.Delete,
  nameof(LivestockCareLog),
  id,
            livestock?.TagCode ?? $"Livestock-{livestockId}",
   $"Xóa nhật ký chăm sóc"
        );
    }
}
