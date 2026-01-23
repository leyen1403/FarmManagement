using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Common;

namespace FarmManagement.Application.Features.ActivityLogs.Queries.GetActivityCountToday;

/// <summary>
/// Handler xử lý GetActivityCountTodayQuery.
/// </summary>
public class GetActivityCountTodayHandler : IQueryHandler<GetActivityCountTodayQuery, int>
{
    private readonly IActivityLogService _activityLogService;

    public GetActivityCountTodayHandler(IActivityLogService activityLogService)
    {
        _activityLogService = activityLogService;
    }

    public Task<int> Handle(GetActivityCountTodayQuery request, CancellationToken cancellationToken)
    {
        var count = _activityLogService.GetActivityCountByDate(DateTime.UtcNow);
        return Task.FromResult(count);
    }
}
