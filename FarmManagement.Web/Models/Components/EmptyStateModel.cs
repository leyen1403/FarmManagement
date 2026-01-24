namespace FarmManagement.Web.Models.Components;

/// <summary>
/// Model cho trạng thái rỗng (empty state) khi không có dữ liệu
/// </summary>
public class EmptyStateModel
{
    /// <summary>
    /// Tiêu đề hiển thị
    /// </summary>
    public string Title { get; set; } = "Không có dữ liệu";

    /// <summary>
    /// Mô tả chi tiết
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Icon hiển thị (CSS class)
    /// </summary>
    public string Icon { get; set; } = "bi bi-inbox";

    /// <summary>
    /// Kích thước icon
    /// </summary>
    public string IconSize { get; set; } = "3rem";

    /// <summary>
    /// CSS class cho màu icon
    /// </summary>
    public string IconColorClass { get; set; } = "text-muted";

    /// <summary>
    /// Hiển thị hình ảnh thay vì icon
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Alt text cho hình ảnh
    /// </summary>
    public string? ImageAlt { get; set; }

    /// <summary>
    /// Chiều rộng hình ảnh
    /// </summary>
    public string ImageWidth { get; set; } = "200px";

    /// <summary>
    /// Action button chính (ví dụ: "Thêm mới")
    /// </summary>
    public ActionButtonModel? PrimaryAction { get; set; }

    /// <summary>
    /// Action button phụ (ví dụ: "Quay lại")
    /// </summary>
    public ActionButtonModel? SecondaryAction { get; set; }

    /// <summary>
    /// CSS class tùy chỉnh
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// Kiểu empty state
    /// </summary>
    public EmptyStateType Type { get; set; } = EmptyStateType.NoData;

    /// <summary>
    /// Constructor mặc định
    /// </summary>
    public EmptyStateModel() { }

    /// <summary>
    /// Constructor với tiêu đề và mô tả
    /// </summary>
    public EmptyStateModel(string title, string? description = null)
    {
        Title = title;
        Description = description;
    }

    /// <summary>
    /// Tạo empty state cho trường hợp không có dữ liệu
    /// </summary>
    public static EmptyStateModel NoData(string? entityName = null, string? createUrl = null)
    {
        var model = new EmptyStateModel
        {
            Title = string.IsNullOrEmpty(entityName) ? "Không có dữ liệu" : $"Chưa có {entityName} nào",
            Description = "Hãy thêm mới để bắt đầu.",
            Icon = "bi bi-inbox",
            Type = EmptyStateType.NoData
        };

        if (!string.IsNullOrEmpty(createUrl))
        {
            model.PrimaryAction = ActionButtonModel.Create(createUrl, "Thêm mới");
        }

        return model;
    }

    /// <summary>
    /// Tạo empty state cho trường hợp không tìm thấy kết quả
    /// </summary>
    public static EmptyStateModel NoSearchResults(string? resetUrl = null)
    {
        var model = new EmptyStateModel
        {
            Title = "Không tìm thấy kết quả",
            Description = "Thử thay đổi từ khóa hoặc bộ lọc tìm kiếm.",
            Icon = "bi bi-search",
            Type = EmptyStateType.NoSearchResults
        };

        if (!string.IsNullOrEmpty(resetUrl))
        {
            model.PrimaryAction = new ActionButtonModel("Xóa bộ lọc", resetUrl, "bi bi-x-circle", "btn-outline-secondary");
        }

        return model;
    }

    /// <summary>
    /// Tạo empty state cho trường hợp lỗi
    /// </summary>
    public static EmptyStateModel Error(string? message = null, string? retryUrl = null)
    {
        var model = new EmptyStateModel
        {
            Title = "Đã xảy ra lỗi",
            Description = message ?? "Không thể tải dữ liệu. Vui lòng thử lại.",
            Icon = "bi bi-exclamation-triangle",
            IconColorClass = "text-danger",
            Type = EmptyStateType.Error
        };

        if (!string.IsNullOrEmpty(retryUrl))
        {
            model.PrimaryAction = new ActionButtonModel("Thử lại", retryUrl, "bi bi-arrow-clockwise", "btn-primary");
        }

        return model;
    }

    /// <summary>
    /// Tạo empty state cho trường hợp không có quyền
    /// </summary>
    public static EmptyStateModel NoPermission(string? backUrl = null)
    {
        var model = new EmptyStateModel
        {
            Title = "Không có quyền truy cập",
            Description = "Bạn không có quyền xem nội dung này.",
            Icon = "bi bi-shield-lock",
            IconColorClass = "text-warning",
            Type = EmptyStateType.NoPermission
        };

        if (!string.IsNullOrEmpty(backUrl))
        {
            model.SecondaryAction = ActionButtonModel.Back(backUrl);
        }

        return model;
    }
}

/// <summary>
/// Enum định nghĩa kiểu empty state
/// </summary>
public enum EmptyStateType
{
    /// <summary>
    /// Không có dữ liệu
    /// </summary>
    NoData,

    /// <summary>
    /// Không tìm thấy kết quả tìm kiếm
    /// </summary>
    NoSearchResults,

    /// <summary>
    /// Lỗi khi tải dữ liệu
    /// </summary>
    Error,

    /// <summary>
    /// Không có quyền truy cập
    /// </summary>
    NoPermission,

    /// <summary>
    /// Tùy chỉnh
    /// </summary>
    Custom
}
