using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Livestocks;

/// <summary>
/// Trạng thái của vật nuôi trong hệ thống.
/// </summary>
public class LivestockStatus : AuditableEntity
{
    /// <summary>
    /// Mã định danh của trạng thái vật nuôi.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã trạng thái vật nuôi.
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Tên trạng thái vật nuôi.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Danh sách các vật nuôi có trạng thái này.
    /// </summary>
    public ICollection<Livestock> Livestocks { get; set; } = new List<Livestock>();
}