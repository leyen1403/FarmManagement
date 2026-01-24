namespace FarmManagement.Web.Models.Components;

/// <summary>
/// Model cho form lọc/tìm kiếm dạng expandable
/// </summary>
public class FilterFormModel
{
    /// <summary>
    /// URL action của form (POST)
    /// </summary>
    public string ActionUrl { get; set; } = string.Empty;

    /// <summary>
    /// HTTP method (GET hoặc POST)
    /// </summary>
    public string Method { get; set; } = "GET";

    /// <summary>
    /// Tiêu đề của form filter
    /// </summary>
    public string Title { get; set; } = "Bộ lọc tìm kiếm";

    /// <summary>
    /// Icon cho tiêu đề
    /// </summary>
    public string Icon { get; set; } = "bi bi-funnel";

    /// <summary>
    /// Form có mở rộng mặc định không
    /// </summary>
    public bool IsExpanded { get; set; } = false;

    /// <summary>
    /// Có thể thu gọn/mở rộng không
    /// </summary>
    public bool IsCollapsible { get; set; } = true;

    /// <summary>
    /// ID duy nhất cho collapse element
    /// </summary>
    public string CollapseId { get; set; } = "filterCollapse";

    /// <summary>
    /// Danh sách các trường filter
    /// </summary>
    public List<FilterFieldModel> Fields { get; set; } = [];

    /// <summary>
    /// Text cho nút tìm kiếm
    /// </summary>
    public string SearchButtonText { get; set; } = "Tìm kiếm";

    /// <summary>
    /// Text cho nút reset
    /// </summary>
    public string ResetButtonText { get; set; } = "Đặt lại";

    /// <summary>
    /// Hiển thị nút reset không
    /// </summary>
    public bool ShowResetButton { get; set; } = true;

    /// <summary>
    /// URL để reset form (thường là trang Index không có query string)
    /// </summary>
    public string? ResetUrl { get; set; }

    /// <summary>
    /// CSS class tùy chỉnh cho form
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// Số cột trong một row (dùng cho grid layout)
    /// </summary>
    public int ColumnsPerRow { get; set; } = 3;
}

/// <summary>
/// Model cho một trường trong form filter
/// </summary>
public class FilterFieldModel
{
    /// <summary>
    /// Tên field (name attribute)
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Label hiển thị
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Kiểu input
    /// </summary>
    public FilterFieldType Type { get; set; } = FilterFieldType.Text;

    /// <summary>
    /// Giá trị hiện tại
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// Placeholder
    /// </summary>
    public string? Placeholder { get; set; }

    /// <summary>
    /// Danh sách options (cho Select, Radio, Checkbox)
    /// </summary>
    public List<SelectOptionModel> Options { get; set; } = [];

    /// <summary>
    /// CSS class cho field wrapper
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// Số cột chiếm (1-12 theo Bootstrap grid)
    /// </summary>
    public int ColSpan { get; set; } = 4;

    /// <summary>
    /// Icon cho input
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Bắt buộc nhập không
    /// </summary>
    public bool IsRequired { get; set; } = false;
}

/// <summary>
/// Enum định nghĩa kiểu field
/// </summary>
public enum FilterFieldType
{
    Text,
    Number,
    Date,
    DateRange,
    Select,
    MultiSelect,
    Checkbox,
    Radio,
    Hidden
}

/// <summary>
/// Model cho option trong select/radio/checkbox
/// </summary>
public class SelectOptionModel
{
    /// <summary>
    /// Giá trị
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Text hiển thị
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Đã chọn chưa
    /// </summary>
    public bool IsSelected { get; set; } = false;

    /// <summary>
    /// Vô hiệu hóa option
    /// </summary>
    public bool IsDisabled { get; set; } = false;

    /// <summary>
    /// Constructor mặc định
    /// </summary>
    public SelectOptionModel() { }

    /// <summary>
    /// Constructor với value và text
    /// </summary>
    public SelectOptionModel(string value, string text, bool isSelected = false)
    {
        Value = value;
        Text = text;
        IsSelected = isSelected;
    }
}
