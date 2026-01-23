using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Locations;

/// <summary>
/// Loại địa điểm trong hệ thống.
/// </summary>
public class LocationType : AuditableEntity
{
    /// <summary>
    /// Mã định danh của loại địa điểm.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã loại địa điểm.
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Tên loại địa điểm.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Danh sách các địa điểm thuộc loại này.
    /// </summary>
    public ICollection<Location> Locations { get; set; } = new List<Location>();
}