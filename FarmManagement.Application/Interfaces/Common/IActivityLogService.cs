using FarmManagement.Application.DTOs.Common;

namespace FarmManagement.Application.Interfaces.Common;

/// <summary>
/// Interface cho service quản lý Activity Log.
/// </summary>
public interface IActivityLogService
{
    /// <summary>
    /// Lấy danh sách hoạt động gần đây.
    /// </summary>
    /// <param name="count">Số lượng hoạt động cần lấy.</param>
    /// <returns>Danh sách ActivityLogDto.</returns>
    IEnumerable<ActivityLogDto> GetRecentActivities(int count = 10);

    /// <summary>
    /// Lấy danh sách hoạt động theo ngày.
    /// </summary>
    /// <param name="date">Ngày cần lấy hoạt động.</param>
    /// <returns>Danh sách ActivityLogDto.</returns>
    IEnumerable<ActivityLogDto> GetActivitiesByDate(DateTime date);

    /// <summary>
    /// Lấy số lượng hoạt động trong ngày.
    /// </summary>
    /// <param name="date">Ngày cần đếm.</param>
    /// <returns>Số lượng hoạt động.</returns>
    int GetActivityCountByDate(DateTime date);

    /// <summary>
    /// Ghi log hoạt động.
    /// </summary>
    /// <param name="dto">Thông tin hoạt động.</param>
    void LogActivity(CreateActivityLogDto dto);

    /// <summary>
    /// Ghi log hoạt động (helper method).
    /// </summary>
    void LogActivity(string actionType, string entityType, int entityId, string entityName, string description, string? performedBy = null);
}
