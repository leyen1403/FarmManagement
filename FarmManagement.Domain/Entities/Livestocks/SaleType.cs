namespace FarmManagement.Domain.Entities.Livestocks;

/// <summary>
/// Phương thức tính tiền bán vật nuôi.
/// </summary>
public enum SaleMethod
{
    /// <summary>
    /// Bán theo con (Số lượng con × Đơn giá/con)
    /// </summary>
    PerHead = 1,

    /// <summary>
    /// Bán theo kg (Trọng lượng × Đơn giá/kg)
    /// </summary>
    PerKilogram = 2,

    /// <summary>
    /// Bán theo lô/đàn (Giá trọn gói)
    /// </summary>
    PerLot = 3,

    /// <summary>
    /// Bán theo bộ/combo (Số bộ × Đơn giá/bộ)
    /// </summary>
    PerSet = 4
}

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
    /// Phương thức tính tiền (PerHead, PerKilogram, PerLot, PerSet)
    /// </summary>
    public SaleMethod SaleMethod { get; set; } = SaleMethod.PerKilogram;

    /// <summary>
    /// Danh sách các giao dịch bán vật nuôi thuộc loại này.
    /// </summary>
    public ICollection<LivestockSale> LivestockSales { get; set; } = new List<LivestockSale>();
}