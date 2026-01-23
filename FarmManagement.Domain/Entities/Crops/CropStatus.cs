using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Crops;

/// <summary>
/// Trạng thái của cây trồng trong hệ thống.
/// </summary>
public class CropStatus : AuditableEntity, IActivatable
{
    /// <summary>
    /// Mã định danh của trạng thái cây trồng.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã trạng thái cây trồng.
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Tên trạng thái cây trồng.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Mô tả trạng thái cây trồng (nếu có).
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Danh sách các cây trồng có trạng thái này.
    /// </summary>
    public ICollection<Crop> Crops { get; set; } = new List<Crop>();

    /// <summary>
    /// Cho biết trạng thái của thực thể có đang hoạt động hay không.
    /// </summary>
    public bool IsActive { get; set; } = true;
}