using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Crops;

/// <summary>
/// Chi phí liên quan đến cây trồng.
/// </summary>
public class CropCost : AuditableEntity
{
    /// <summary>
    /// Mã định danh của chi phí.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã định danh cây trồng.
    /// </summary>
    public int CropId { get; set; }

    /// <summary>
    /// Mã định danh loại chi phí.
    /// </summary>
    public int CostTypeId { get; set; }

    /// <summary>
    /// Ngày phát sinh chi phí.
    /// </summary>
    public DateTime CostDate { get; set; }

    /// <summary>
    /// Số lượng chi phí.
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    /// Đơn vị đo lường (nếu có).
    /// </summary>
    public string? Unit { get; set; }

    /// <summary>
    /// Giá đơn vị của chi phí.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Tổng giá trị chi phí.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Ghi chú về chi phí (nếu có).
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Cây trồng liên quan đến chi phí.
    /// </summary>
    public Crop Crop { get; set; } = null!;

    /// <summary>
    /// Loại chi phí liên quan.
    /// </summary>
    public CostType CostType { get; set; } = null!;
}