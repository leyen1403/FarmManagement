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
    /// Tên entity bị ảnh hưởng (Location, Crop, CropType, etc.).
    /// </summary>
    public string EntityType { get; set; } = null!;

    /// <summary>
    /// ID của entity bị ảnh hưởng.
    /// </summary>
    public int EntityId { get; set; }

    /// <summary>
    /// Tên/mô tả của entity bị ảnh hưởng.
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
    public const string Create = "Create";
    public const string Update = "Update";
    public const string Delete = "Delete";
}

/// <summary>
/// Các loại entity trong hệ thống.
/// </summary>
public static class ActivityEntityTypes
{
    public const string Location = "Location";
    public const string LocationType = "LocationType";
    public const string LocationStatus = "LocationStatus";
    public const string Crop = "Crop";
    public const string CropType = "CropType";
    public const string CropStatus = "CropStatus";
    public const string CropPrice = "CropPrice";
    public const string CropHarvest = "CropHarvest";
    public const string CropCost = "CropCost";
    public const string CropCareLog = "CropCareLog";
    public const string CostType = "CostType";
    public const string CropCareType = "CropCareType";
}
