using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Livestocks;

/// <summary>
/// Đơn hàng bán vật nuôi (Order Header).
/// </summary>
public class LivestockSale : AuditableEntity
{
    /// <summary>
    /// Mã định danh của đơn hàng.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã đơn hàng (tự động tạo hoặc nhập).
    /// </summary>
    public string? OrderCode { get; set; }

    /// <summary>
    /// Mã định danh vật nuôi.
    /// </summary>
    public int LivestockId { get; set; }

    /// <summary>
    /// Mã định danh loại bán vật nuôi.
    /// </summary>
    public int SaleTypeId { get; set; }

    /// <summary>
    /// Ngày thực hiện giao dịch bán.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Tổng số lượng con bán (tính từ chi tiết).
    /// </summary>
    public int TotalQuantity { get; set; }

    /// <summary>
    /// Tổng trọng lượng bán (tính từ chi tiết).
    /// </summary>
    public decimal TotalWeight { get; set; }

    /// <summary>
    /// Tổng giá trị đơn hàng (tính từ chi tiết).
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Tên người mua.
    /// </summary>
    public string? Buyer { get; set; }

    /// <summary>
    /// Số điện thoại người mua.
    /// </summary>
    public string? BuyerPhone { get; set; }

    /// <summary>
    /// Ghi chú về đơn hàng.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Trạng thái đơn hàng (Draft, Completed, Cancelled).
    /// </summary>
    public OrderStatus Status { get; set; } = OrderStatus.Completed;

    /// <summary>
    /// Ngày tạo đơn hàng.
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Vật nuôi liên quan đến đơn hàng.
    /// </summary>
    public Livestock Livestock { get; set; } = null!;

    /// <summary>
    /// Loại bán vật nuôi (chỉ chọn 1 loại cho mỗi Livestock).
    /// </summary>
    public SaleType SaleType { get; set; } = null!;

    /// <summary>
    /// Danh sách chi tiết đơn hàng (các lượt cân).
    /// </summary>
    public ICollection<LivestockSaleDetail> Details { get; set; } = new List<LivestockSaleDetail>();
}

/// <summary>
/// Trạng thái đơn hàng bán.
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// Nháp - đang nhập liệu
    /// </summary>
    Draft = 0,

    /// <summary>
    /// Hoàn thành
    /// </summary>
    Completed = 1,

    /// <summary>
    /// Đã hủy
    /// </summary>
    Cancelled = 2
}