using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Locations;

/// <summary>
/// Trạng thái của địa điểm trong hệ thống.
/// </summary>
public class LocationStatus : AuditableEntity
{
    /// <summary>
    /// Mã định danh của trạng thái địa điểm.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã trạng thái địa điểm.
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Tên trạng thái địa điểm.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Danh sách các địa điểm có trạng thái này.
    /// </summary>
    public ICollection<Location> Locations { get; set; } = new List<Location>();
}