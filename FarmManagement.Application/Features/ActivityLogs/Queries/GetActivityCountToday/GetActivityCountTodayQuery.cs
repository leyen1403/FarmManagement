using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.ActivityLogs.Queries.GetActivityCountToday;

/// <summary>
/// Query lấy số lượng hoạt động trong ngày.
/// </summary>
public record GetActivityCountTodayQuery() : IQuery<int>;
