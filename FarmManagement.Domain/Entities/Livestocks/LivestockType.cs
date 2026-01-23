using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Livestocks;

/// <summary>
/// Loại vật nuôi trong hệ thống.
/// </summary>
public class LivestockType : AuditableEntity
{
    /// <summary>
    /// Mã định danh của loại vật nuôi.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã loại vật nuôi.
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Tên loại vật nuôi.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Mô tả chi tiết về loại vật nuôi (nếu có).
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Danh sách các vật nuôi thuộc loại này.
    /// </summary>
    public ICollection<Livestock> Livestocks { get; set; } = new List<Livestock>();
}