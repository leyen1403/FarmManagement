using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Crops;

/// <summary>
/// Loại chi phí liên quan đến cây trồng.
/// </summary>
public class CostType : AuditableEntity
{
    /// <summary>
    /// Mã định danh của loại chi phí.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã loại chi phí.
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Tên loại chi phí.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Mô tả loại chi phí.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Trạng thái hoạt động.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Danh sách các chi phí thuộc loại này.
    /// </summary>
    public ICollection<CropCost> CropCosts { get; set; } = new List<CropCost>();
}