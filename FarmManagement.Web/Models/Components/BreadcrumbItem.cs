namespace FarmManagement.Web.Models.Components;

/// <summary>
/// Model đại diện cho một item trong breadcrumb navigation
/// </summary>
public class BreadcrumbItem
{
    /// <summary>
    /// Tiêu đề hiển thị của breadcrumb item
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// URL điều hướng (null nếu là item hiện tại)
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Icon hiển thị (CSS class, ví dụ: "fas fa-home")
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Đánh dấu item này là trang hiện tại (active)
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Thứ tự hiển thị
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Constructor mặc định
    /// </summary>
    public BreadcrumbItem()
    {
    }

    /// <summary>
    /// Constructor với các tham số cơ bản
    /// </summary>
    /// <param name="title">Tiêu đề</param>
    /// <param name="url">URL (null nếu là trang hiện tại)</param>
    /// <param name="isActive">Có phải trang hiện tại không</param>
    public BreadcrumbItem(string title, string? url = null, bool isActive = false)
    {
        Title = title;
        Url = url;
        IsActive = isActive;
    }

    /// <summary>
    /// Constructor đầy đủ
    /// </summary>
    /// <param name="title">Tiêu đề</param>
    /// <param name="url">URL</param>
    /// <param name="icon">Icon CSS class</param>
    /// <param name="isActive">Có phải trang hiện tại không</param>
    public BreadcrumbItem(string title, string? url, string? icon, bool isActive = false)
    {
        Title = title;
        Url = url;
        Icon = icon;
        IsActive = isActive;
    }

    /// <summary>
    /// Tạo breadcrumb item cho trang Home
    /// </summary>
    public static BreadcrumbItem Home(string url = "/") =>  new("Trang chủ", url, "fas fa-home", false);

    /// <summary>
    /// Tạo breadcrumb item cho trang hiện tại (không có link)
    /// </summary>
    public static BreadcrumbItem Current(string title, string? icon = null) =>
        new(title, null, icon, true);
}
