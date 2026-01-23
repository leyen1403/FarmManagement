using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Crops;

/// <summary>
/// Loại cây trồng trong hệ thống.
/// </summary>
public class CropType : AuditableEntity, IActivatable
{
    /// <summary>
    /// Mã định danh của loại cây trồng.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã loại cây trồng.
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Tên loại cây trồng.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Mô tả chi tiết về loại cây trồng (nếu có).
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Danh sách các cây trồng thuộc loại này.
    /// </summary>
    public ICollection<Crop> Crops { get; set; } = new List<Crop>();
    public bool IsActive { get; set; } = true;
}