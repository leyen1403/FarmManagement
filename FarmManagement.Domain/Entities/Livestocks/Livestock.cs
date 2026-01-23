using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Livestocks;

/// <summary>
/// Đại diện cho vật nuôi trong hệ thống.
/// </summary>
public class Livestock : AuditableEntity
{
    /// <summary>
    /// Mã định danh của vật nuôi.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã định danh loại vật nuôi.
    /// </summary>
    public int LivestockTypeId { get; set; }

    /// <summary>
    /// Mã định danh địa điểm nuôi.
    /// </summary>
    public int LocationId { get; set; }

    /// <summary>
    /// Mã định danh trạng thái vật nuôi.
    /// </summary>
    public int LivestockStatusId { get; set; }

    /// <summary>
    /// Mã thẻ nhận dạng của vật nuôi (nếu có).
    /// </summary>
    public string? TagCode { get; set; }

    /// <summary>
    /// Ngày nhập vật nuôi.
    /// </summary>
    public DateTime ImportDate { get; set; }

    /// <summary>
    /// Trọng lượng nhập vật nuôi.
    /// </summary>
    public decimal ImportWeight { get; set; }

    /// <summary>
    /// Giá nhập vật nuôi.
    /// </summary>
    public decimal ImportPrice { get; set; }

    /// <summary>
    /// Ghi chú về vật nuôi (nếu có).
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Loại vật nuôi.
    /// </summary>
    public LivestockType LivestockType { get; set; } = null!;

    /// <summary>
    /// Trạng thái vật nuôi.
    /// </summary>
    public LivestockStatus LivestockStatus { get; set; } = null!;

    /// <summary>
    /// Nhật ký chăm sóc vật nuôi.
    /// </summary>
    public ICollection<LivestockCareLog> LivestockCareLogs { get; set; }

    /// <summary>
    /// Nhật ký sức khỏe vật nuôi.
    /// </summary>
    public ICollection<LivestockHealthLog> LivestockHealthLogs { get; set; }

    /// <summary>
    /// Danh sách bán vật nuôi.
    /// </summary>
    public ICollection<LivestockSale> LivestockSales { get; set; }
}