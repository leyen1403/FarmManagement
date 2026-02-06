using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Common;

/// <summary>
/// Entity lưu trữ lịch sử hoạt động trong hệ thống.
/// </summary>
public class ActivityLog : AuditableEntity
{
    /// <summary>
    /// Mã định danh của activity log.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Loại hoạt động (Create, Update, Delete).
    /// </summary>
    public string ActionType { get; set; } = null!;

    /// <summary>
    /// Tên entity bị ảnh hưởng (Location, Crop, CropType, v.v.).
    /// </summary>
    public string EntityType { get; set; } = null!;

    /// <summary>
    /// ID của entity bị ảnh hưởng.
    /// </summary>
    public int EntityId { get; set; }

    /// <summary>
    /// Tên hoặc mô tả của entity bị ảnh hưởng.
    /// </summary>
    public string EntityName { get; set; } = null!;

    /// <summary>
    /// Mô tả chi tiết hoạt động.
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Thời gian thực hiện hoạt động.
    /// </summary>
    public DateTime ActionDate { get; set; }

    /// <summary>
    /// Tên người thực hiện (nếu có).
    /// </summary>
    public string? PerformedBy { get; set; }
}

/// <summary>
/// Các loại hoạt động trong hệ thống.
/// </summary>
public static class ActivityActionTypes
{
    /// <summary>
    /// Hành động tạo mới.
    /// </summary>
    public const string Create = "Create";
    /// <summary>
    /// Hành động cập nhật.
    /// </summary>
    public const string Update = "Update";
    /// <summary>
    /// Hành động xóa.
    /// </summary>
    public const string Delete = "Delete";
}

/// <summary>
/// Các loại entity trong hệ thống.
/// </summary>
public static class ActivityEntityTypes
{
    /// <summary>
    /// Đối tượng Location.
    /// </summary>
    public const string Location = "Location";
    /// <summary>
    /// Đối tượng LocationType.
    /// </summary>
    public const string LocationType = "LocationType";
    /// <summary>
    /// Đối tượng LocationStatus.
    /// </summary>
    public const string LocationStatus = "LocationStatus";
    /// <summary>
    /// Đối tượng Crop.
    /// </summary>
    public const string Crop = "Crop";
    /// <summary>
    /// Đối tượng CropType.
    /// </summary>
    public const string CropType = "CropType";
    /// <summary>
    /// Đối tượng CropStatus.
    /// </summary>
    public const string CropStatus = "CropStatus";
    /// <summary>
    /// Đối tượng CropPrice.
    /// </summary>
    public const string CropPrice = "CropPrice";
    /// <summary>
    /// Đối tượng CropHarvest.
    /// </summary>
    public const string CropHarvest = "CropHarvest";
    /// <summary>
    /// Đối tượng CropCost.
    /// </summary>
    public const string CropCost = "CropCost";
    /// <summary>
    /// Đối tượng CropCareLog.
    /// </summary>
    public const string CropCareLog = "CropCareLog";
    /// <summary>
    /// Đối tượng CostType.
    /// </summary>
    public const string CostType = "CostType";
    /// <summary>
    /// Đối tượng CropCareType.
    /// </summary>
    public const string CropCareType = "CropCareType";
}
