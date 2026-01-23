using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Crops;

/// <summary>
/// Giá cây trồng theo thời điểm - lưu lịch sử thay đổi giá.
/// </summary>
public class CropPrice : AuditableEntity
{
    /// <summary>
    /// Mã định danh của giá cây trồng.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã định danh cây trồng.
    /// </summary>
    public int CropId { get; set; }

    /// <summary>
    /// Giá bán (VNĐ).
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Đơn vị tính giá (kg, tấn, cây...).
    /// </summary>
    public string Unit { get; set; } = null!;

    /// <summary>
    /// Ngày áp dụng giá.
    /// </summary>
    public DateTime EffectiveDate { get; set; }

    /// <summary>
    /// Ngày hết hạn giá (nếu có).
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// Ghi chú về giá (nếu có).
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Trạng thái hoạt động.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Cây trồng liên quan.
    /// </summary>
    public Crop Crop { get; set; } = null!;
}
