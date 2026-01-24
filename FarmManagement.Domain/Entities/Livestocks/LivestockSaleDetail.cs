namespace FarmManagement.Domain.Entities.Livestocks;

/// <summary>
/// Chi tiết dòng bán trong đơn hàng (mỗi lượt cân).
/// </summary>
public class LivestockSaleDetail
{
    /// <summary>
    /// Mã định danh chi tiết bán.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã đơn hàng (LivestockSale).
    /// </summary>
    public int LivestockSaleId { get; set; }

    /// <summary>
    /// Giới tính vật nuôi bán (Đực/Cái).
    /// </summary>
    public GenderType Gender { get; set; }

    /// <summary>
    /// Số lượng con trong lượt cân này.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Trọng lượng (kg) của lượt cân này.
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// Đơn giá (VNĐ/kg) - có thể khác nhau giữa Đực và Cái.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Thành tiền = Weight × UnitPrice.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Ghi chú cho lượt cân này.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Số thứ tự dòng trong đơn hàng.
    /// </summary>
    public int LineNumber { get; set; }

    /// <summary>
    /// Navigation property - Đơn hàng chứa chi tiết này.
    /// </summary>
    public LivestockSale LivestockSale { get; set; } = null!;
}

/// <summary>
/// Giới tính vật nuôi.
/// </summary>
public enum GenderType
{
    /// <summary>
    /// Đực
    /// </summary>
    Male = 1,

    /// <summary>
    /// Cái
    /// </summary>
    Female = 2,

    /// <summary>
    /// Hỗn hợp (không phân biệt)
    /// </summary>
    Mixed = 3
}
