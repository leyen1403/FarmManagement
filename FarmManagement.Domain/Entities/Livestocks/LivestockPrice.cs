using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Livestocks;

/// <summary>
/// Bảng giá bán vật nuôi theo kg - theo giới tính và thời gian hiệu lực.
/// </summary>
public class LivestockPrice : AuditableEntity
{
    /// <summary>
    /// Mã định danh.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã vật nuôi (liên kết với Livestock).
    /// </summary>
    public int LivestockId { get; set; }

    /// <summary>
    /// Giới tính áp dụng (Đực/Cái/Hỗn hợp).
    /// </summary>
    public GenderType Gender { get; set; }

    /// <summary>
    /// Đơn giá (VNĐ/kg).
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Ngày bắt đầu hiệu lực.
    /// </summary>
    public DateTime EffectiveFrom { get; set; }

    /// <summary>
    /// Ngày kết thúc hiệu lực (null = vô thời hạn).
    /// </summary>
    public DateTime? EffectiveTo { get; set; }

    /// <summary>
    /// Trạng thái kích hoạt.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Ghi chú.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Ngày tạo.
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Ngày cập nhật.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Navigation property - Vật nuôi.
    /// </summary>
    public Livestock Livestock { get; set; } = null!;

    /// <summary>
    /// Kiểm tra giá có hiệu lực tại ngày cụ thể không.
    /// </summary>
    public bool IsEffectiveAt(DateTime date)
    {
        if (!IsActive)
            return false;
        if (date < EffectiveFrom)
            return false;
        if (EffectiveTo.HasValue && date > EffectiveTo.Value)
            return false;
        return true;
    }
}
