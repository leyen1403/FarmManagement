namespace FarmManagement.Domain.Entities.Livestocks;

/// <summary>
/// Giao dịch bán vật nuôi.
/// </summary>
public class LivestockSale
{
    /// <summary>
    /// Mã định danh của giao dịch bán.
    /// </summary>
    public int Id { get; set; }

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
    /// Trọng lượng vật nuôi được bán.
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// Giá đơn vị của vật nuôi.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Tổng giá trị giao dịch bán.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Tên người mua (nếu có).
    /// </summary>
    public string? Buyer { get; set; }

    /// <summary>
    /// Ghi chú về giao dịch bán (nếu có).
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Vật nuôi liên quan đến giao dịch bán.
    /// </summary>
    public Livestock Livestock { get; set; } = null!;

    /// <summary>
    /// Loại bán vật nuôi liên quan.
    /// </summary>
    public SaleType SaleType { get; set; } = null!;
}