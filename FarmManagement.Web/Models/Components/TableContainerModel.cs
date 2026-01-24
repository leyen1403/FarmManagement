namespace FarmManagement.Web.Models.Components;

/// <summary>
/// Model cho table container với toolbar và các tính năng
/// </summary>
public class TableContainerModel
{
    /// <summary>
    /// Tiêu đề của table
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Mô tả/subtitle
    /// </summary>
    public string? Subtitle { get; set; }

    /// <summary>
    /// Icon cho tiêu đề
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Hiển thị toolbar không
    /// </summary>
    public bool ShowToolbar { get; set; } = true;

    /// <summary>
    /// Hiển thị thanh tìm kiếm nhanh trong toolbar
    /// </summary>
    public bool ShowQuickSearch { get; set; } = true;

    /// <summary>
    /// Placeholder cho quick search
    /// </summary>
    public string QuickSearchPlaceholder { get; set; } = "Tìm kiếm...";

    /// <summary>
    /// Giá trị tìm kiếm hiện tại
    /// </summary>
    public string? SearchValue { get; set; }

    /// <summary>
    /// Tên parameter cho search
    /// </summary>
    public string SearchParameterName { get; set; } = "searchTerm";

    /// <summary>
    /// Hiển thị badge tổng số bản ghi
    /// </summary>
    public bool ShowTotalBadge { get; set; } = true;

    /// <summary>
    /// Tổng số bản ghi
    /// </summary>
    public int TotalRecords { get; set; } = 0;

    /// <summary>
    /// Text cho badge (ví dụ: "bản ghi", "mục", "dòng")
    /// </summary>
    public string TotalBadgeText { get; set; } = "bản ghi";

    /// <summary>
    /// Danh sách các action buttons trên toolbar
    /// </summary>
    public List<ActionButtonModel> ToolbarActions { get; set; } = [];

    /// <summary>
    /// CSS class cho table
    /// </summary>
    public string TableCssClass { get; set; } = "table table-hover table-striped align-middle";

    /// <summary>
    /// ID của table
    /// </summary>
    public string TableId { get; set; } = "dataTable";

    /// <summary>
    /// Responsive wrapper
    /// </summary>
    public bool IsResponsive { get; set; } = true;

    /// <summary>
    /// Hiển thị border cho table
    /// </summary>
    public bool ShowBorder { get; set; } = false;

    /// <summary>
    /// Hiển thị EmptyState khi không có dữ liệu
    /// </summary>
    public bool HasData { get; set; } = true;

    /// <summary>
    /// Model cho empty state
    /// </summary>
    public EmptyStateModel? EmptyState { get; set; }

    /// <summary>
    /// CSS class tùy chỉnh cho container
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// Danh sách các filter badges đang active
    /// </summary>
    public List<FilterBadgeModel> ActiveFilters { get; set; } = [];
}

/// <summary>
/// Model cho badge hiển thị filter đang active
/// </summary>
public class FilterBadgeModel
{
    /// <summary>
    /// Tên field
    /// </summary>
    public string FieldName { get; set; } = string.Empty;

    /// <summary>
    /// Label hiển thị
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Giá trị hiển thị
    /// </summary>
    public string DisplayValue { get; set; } = string.Empty;

    /// <summary>
    /// URL để xóa filter này
    /// </summary>
    public string? RemoveUrl { get; set; }

    /// <summary>
    /// CSS class cho badge
    /// </summary>
    public string CssClass { get; set; } = "bg-primary";

    /// <summary>
    /// Constructor mặc định
    /// </summary>
    public FilterBadgeModel() { }

    /// <summary>
    /// Constructor với các tham số
    /// </summary>
    public FilterBadgeModel(string label, string displayValue, string? removeUrl = null)
    {
        Label = label;
        DisplayValue = displayValue;
        RemoveUrl = removeUrl;
    }
}
