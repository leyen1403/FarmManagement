using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Crops;

/// <summary>
/// Đại diện cho thông tin thu hoạch cây trồng.
/// </summary>
public class CropHarvest : AuditableEntity
{
    /// <summary>
    /// Mã định danh của thông tin thu hoạch.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã định danh cây trồng.
    /// </summary>
    public int CropId { get; set; }

    /// <summary>
    /// Ngày thu hoạch.
    /// </summary>
    public DateTime HarvestDate { get; set; }

    /// <summary>
    /// Số lượng thu hoạch.
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    /// Đơn vị đo lường (nếu có).
    /// </summary>
    public string? Unit { get; set; }

    /// <summary>
    /// Giá đơn vị của sản phẩm thu hoạch.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Tổng giá trị thu hoạch.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Tên người mua (nếu có).
    /// </summary>
    public string? Buyer { get; set; }

    /// <summary>
    /// Ghi chú về thông tin thu hoạch (nếu có).
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Cây trồng liên quan đến thông tin thu hoạch.
    /// </summary>
    public Crop Crop { get; set; } = null!;
}