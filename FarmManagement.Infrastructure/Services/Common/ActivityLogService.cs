using FarmManagement.Application.DTOs.Common;
using FarmManagement.Application.Interfaces.Common;
using FarmManagement.Domain.Entities.Common;
using FarmManagement.Infrastructure.Persistence.DbContext;

namespace FarmManagement.Infrastructure.Services.Common;

public class ActivityLogService : IActivityLogService
{
    private readonly FarmManagementDbContext _context;

    public ActivityLogService(FarmManagementDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ActivityLogDto> GetRecentActivities(int count = 10)
    {
        var activities = _context.ActivityLogs
            .OrderByDescending(a => a.ActionDate)
            .Take(count)
            .ToList();

        return activities.Select(MapToDto);
    }

    public IEnumerable<ActivityLogDto> GetActivitiesByDate(DateTime date)
    {
        var startOfDay = date.Date;
        var endOfDay = startOfDay.AddDays(1);

        var activities = _context.ActivityLogs
            .Where(a => a.ActionDate >= startOfDay && a.ActionDate < endOfDay)
            .OrderByDescending(a => a.ActionDate)
            .ToList();

        return activities.Select(MapToDto);
    }

    public int GetActivityCountByDate(DateTime date)
    {
        var startOfDay = date.Date;
        var endOfDay = startOfDay.AddDays(1);

        return _context.ActivityLogs
            .Count(a => a.ActionDate >= startOfDay && a.ActionDate < endOfDay);
    }

    public void LogActivity(CreateActivityLogDto dto)
    {
        var log = new ActivityLog
        {
            ActionType = dto.ActionType,
            EntityType = dto.EntityType,
            EntityId = dto.EntityId,
            EntityName = dto.EntityName,
            Description = dto.Description,
            ActionDate = DateTime.UtcNow,
            PerformedBy = dto.PerformedBy
        };

        _context.ActivityLogs.Add(log);
        _context.SaveChanges();
    }

    public void LogActivity(string actionType, string entityType, int entityId, string entityName, string description, string? performedBy = null)
    {
        LogActivity(new CreateActivityLogDto
        {
            ActionType = actionType,
            EntityType = entityType,
            EntityId = entityId,
            EntityName = entityName,
            Description = description,
            PerformedBy = performedBy
        });
    }

    private static ActivityLogDto MapToDto(ActivityLog entity)
    {
        return new ActivityLogDto
        {
            Id = entity.Id,
            ActionType = entity.ActionType,
            EntityType = entity.EntityType,
            EntityId = entity.EntityId,
            EntityName = entity.EntityName,
            Description = entity.Description,
            ActionDate = entity.ActionDate,
            PerformedBy = entity.PerformedBy
        };
    }
}