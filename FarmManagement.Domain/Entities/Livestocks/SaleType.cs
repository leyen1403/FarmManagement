namespace FarmManagement.Domain.Entities.Livestocks;

/// <summary>
/// Loại bán vật nuôi trong hệ thống.
/// </summary>
public class SaleType
{
    /// <summary>
    /// Mã định danh của loại bán vật nuôi.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã loại bán vật nuôi.
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Tên loại bán vật nuôi.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Danh sách các giao dịch bán vật nuôi thuộc loại này.
    /// </summary>
    public ICollection<LivestockSale> LivestockSales { get; set; } = new List<LivestockSale>();
}