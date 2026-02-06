using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Livestocks;

/// <summary>
/// Trạng thái sức khỏe của vật nuôi trong hệ thống.
/// </summary>
public class LivestockHealthStatus : AuditableEntity
{
    /// <summary>
    /// Mã định danh của trạng thái sức khỏe vật nuôi.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã trạng thái sức khỏe vật nuôi.
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Tên trạng thái sức khỏe vật nuôi.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Danh sách các nhật ký sức khỏe vật nuôi có trạng thái này.
    /// </summary>
    public ICollection<LivestockHealthLog> LivestockHealthLogs { get; set; } = new List<LivestockHealthLog>();
}