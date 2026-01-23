using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Locations;

/// <summary>
/// Đại diện cho một địa điểm trong hệ thống.
/// </summary>
public class Location : AuditableEntity
{
    /// <summary>
    /// Mã định danh của địa điểm.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Tên của địa điểm.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Mã định danh loại địa điểm.
    /// </summary>
    public int LocationTypeId { get; set; }

    /// <summary>
    /// Mã định danh trạng thái địa điểm.
    /// </summary>
    public int LocationStatusId { get; set; }

    /// <summary>
    /// Diện tích của địa điểm (nếu có).
    /// </summary>
    public decimal? Area { get; set; }

    /// <summary>
    /// Sức chứa của địa điểm (nếu có).
    /// </summary>
    public decimal? Capacity { get; set; }

    /// <summary>
    /// Đơn vị đo sức chứa (nếu có).
    /// </summary>
    public string? CapacityUnit { get; set; }

    /// <summary>
    /// Ngày bắt đầu sử dụng địa điểm (nếu có).
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Ngày kết thúc sử dụng địa điểm (nếu có).
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Mã định danh địa điểm cha (nếu có).
    /// </summary>
    public int? ParentLocationId { get; set; }

    /// <summary>
    /// Ghi chú về địa điểm (nếu có).
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Địa chỉ của địa điểm.
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    /// Mô tả về địa điểm.
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Loại của địa điểm.
    /// </summary>
    public LocationType LocationType { get; set; } = null!;

    /// <summary>
    /// Trạng thái của địa điểm.
    /// </summary>
    public LocationStatus LocationStatus { get; set; } = null!;

    /// <summary>
    /// Địa điểm cha (nếu có).
    /// </summary>
    public Location? ParentLocation { get; set; }

    /// <summary>
    /// Danh sách các địa điểm con.
    /// </summary>
    public ICollection<Location> ChildLocations { get; set; } = new List<Location>();
}