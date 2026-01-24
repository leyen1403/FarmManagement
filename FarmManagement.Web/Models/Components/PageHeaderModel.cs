namespace FarmManagement.Web.Models.Components;

/// <summary>
/// Model đại diện cho header của trang
/// </summary>
public class PageHeaderModel
{
    /// <summary>
    /// Tiêu đề chính của trang
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Tiêu đề phụ/mô tả ngắn
    /// </summary>
    public string? Subtitle { get; set; }

    /// <summary>
    /// Icon hiển thị bên cạnh tiêu đề (CSS class, ví dụ: "fas fa-home")
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// CSS class tùy chỉnh cho header
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// Danh sách các action buttons hiển thị trên header
    /// </summary>
    public List<ActionButtonModel> Actions { get; set; } = [];

    /// <summary>
    /// Hiển thị breadcrumb hay không
    /// </summary>
    public bool ShowBreadcrumb { get; set; } = true;

    /// <summary>
    /// Danh sách breadcrumb items
    /// </summary>
    public List<BreadcrumbItem> Breadcrumbs { get; set; } = [];
}
