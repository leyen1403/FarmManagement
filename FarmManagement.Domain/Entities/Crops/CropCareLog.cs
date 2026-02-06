using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Crops;

/// <summary>
/// Nhật ký chăm sóc cây trồng.
/// </summary>
public class CropCareLog : AuditableEntity
{
    /// <summary>
    /// Mã định danh của nhật ký chăm sóc.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã định danh cây trồng.
    /// </summary>
    public int CropId { get; set; }

    /// <summary>
    /// Mã định danh loại chăm sóc cây trồng.
    /// </summary>
    public int CropCareTypeId { get; set; }

    /// <summary>
    /// Ngày thực hiện chăm sóc.
    /// </summary>
    public DateTime CareDate { get; set; }

    /// <summary>
    /// Mô tả chi tiết về hoạt động chăm sóc (nếu có).
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Chi phí chăm sóc.
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// Cây trồng liên quan đến nhật ký chăm sóc.
    /// </summary>
    public Crop Crop { get; set; } = null!;

    /// <summary>
    /// Loại chăm sóc cây trồng liên quan.
    /// </summary>
    public CropCareType CropCareType { get; set; } = null!;

    /// <summary>
    /// Danh sách các nhật ký chăm sóc cây trồng liên quan.
    /// </summary>
    public ICollection<CropCareLog> CropCareLogs { get; set; } = null!;
}