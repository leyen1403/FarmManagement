using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Crops;

/// <summary>
/// Loại chăm sóc cây trồng trong hệ thống.
/// </summary>
public class CropCareType : AuditableEntity
{
    /// <summary>
    /// Mã định danh của loại chăm sóc cây trồng.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã loại chăm sóc cây trồng.
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Tên loại chăm sóc cây trồng.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Mô tả loại chăm sóc.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Trạng thái hoạt động.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Danh sách các nhật ký chăm sóc cây trồng thuộc loại này.
    /// </summary>
    public ICollection<CropCareLog> CropCareLogs { get; set; } = new List<CropCareLog>();
}