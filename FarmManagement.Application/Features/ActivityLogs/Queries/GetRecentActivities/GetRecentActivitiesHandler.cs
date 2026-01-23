using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Common;
using FarmManagement.Application.Interfaces.Common;

namespace FarmManagement.Application.Features.ActivityLogs.Queries.GetRecentActivities;

/// <summary>
/// Handler xử lý GetRecentActivitiesQuery.
/// </summary>
public class GetRecentActivitiesHandler : IQueryHandler<GetRecentActivitiesQuery, IEnumerable<ActivityLogDto>>
{
    private readonly IActivityLogService _activityLogService;

    public GetRecentActivitiesHandler(IActivityLogService activityLogService)
    {
        _activityLogService = activityLogService;
    }

    public Task<IEnumerable<ActivityLogDto>> Handle(GetRecentActivitiesQuery request, CancellationToken cancellationToken)
    {
        var result = _activityLogService.GetRecentActivities(request.Count);
        return Task.FromResult(result);
    }
}
