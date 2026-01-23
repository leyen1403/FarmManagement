using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Common;

namespace FarmManagement.Application.Features.ActivityLogs.Queries.GetRecentActivities;

/// <summary>
/// Query lấy danh sách hoạt động gần đây.
/// </summary>
/// <param name="Count">Số lượng hoạt động cần lấy.</param>
public record GetRecentActivitiesQuery(int Count = 10) : IQuery<IEnumerable<ActivityLogDto>>;
