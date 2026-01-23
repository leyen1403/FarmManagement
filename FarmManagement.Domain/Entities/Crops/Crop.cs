namespace FarmManagement.Domain.Entities.Crops;
using FarmManagement.Domain.Common;
using FarmManagement.Domain.Entities.Locations;

/// <summary>
/// Đại diện cho cây trồng trong hệ thống.
/// </summary>
public class Crop : AuditableEntity
{
    /// <summary>
    /// Mã định danh của cây trồng.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Tên cây trồng.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Mã định danh loại cây trồng.
    /// </summary>
    public int CropTypeId { get; set; }

    /// <summary>
    /// Mã định danh địa điểm trồng cây.
    /// </summary>
    public int LocationId { get; set; }

    /// <summary>
    /// Mã định danh trạng thái cây trồng.
    /// </summary>
    public int CropStatusId { get; set; }

    /// <summary>
    /// Ngày trồng cây.
    /// </summary>
    public DateTime PlantDate { get; set; }

    /// <summary>
    /// Ngày dự kiến thu hoạch (nếu có).
    /// </summary>
    public DateTime? ExpectedHarvestDate { get; set; }

    /// <summary>
    /// Ngày thu hoạch thực tế (nếu có).
    /// </summary>
    public DateTime? ActualHarvestDate { get; set; }

    /// <summary>
    /// Sản lượng ước tính (nếu có).
    /// </summary>
    public decimal? EstimatedYield { get; set; }

    /// <summary>
    /// Đơn vị đo sản lượng (nếu có).
    /// </summary>
    public string? Unit { get; set; }

    /// <summary>
    /// Ghi chú về cây trồng (nếu có).
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Loại cây trồng.
    /// </summary>
    public CropType CropType { get; set; } = null!;

    /// <summary>
    /// Trạng thái cây trồng.
    /// </summary>
    public CropStatus CropStatus { get; set; } = null!;

    /// <summary>
    /// Địa điểm trồng cây.
    /// </summary>
    public Location Location { get; set; } = null!;

    /// <summary>
    /// Nhật ký chăm sóc cây trồng.
    /// </summary>
    public ICollection<CropCareLog> CropCareLogs { get; set; } = new List<CropCareLog>();

    /// <summary>
    /// Danh sách thu hoạch cây trồng.
    /// </summary>
    public ICollection<CropHarvest> CropHarvests { get; set; } = new List<CropHarvest>();

    /// <summary>
    /// Danh sách chi phí liên quan đến cây trồng.
    /// </summary>
    public ICollection<CropCost> CropCosts { get; set; } = new List<CropCost>();

    /// <summary>
    /// Danh sách giá cây trồng.
    /// </summary>
    public ICollection<CropPrice> CropPrices { get; set; } = new List<CropPrice>();
}