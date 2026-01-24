namespace FarmManagement.Web.Models.Components;

/// <summary>
/// Model đại diện cho một action button
/// </summary>
public class ActionButtonModel
{
    /// <summary>
    /// Text hiển thị trên button
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// URL điều hướng khi click
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Icon hiển thị (CSS class, ví dụ: "fas fa-plus")
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// CSS class cho button (ví dụ: "btn-primary", "btn-success", "btn-outline-secondary")
    /// </summary>
    public string CssClass { get; set; } = "btn-primary";

    /// <summary>
    /// Kiểu button
    /// </summary>
    public ButtonType Type { get; set; } = ButtonType.Link;

    /// <summary>
    /// Target cho link (_blank, _self, etc.)
    /// </summary>
    public string? Target { get; set; }

    /// <summary>
    /// Tooltip hiển thị khi hover
    /// </summary>
    public string? Tooltip { get; set; }

    /// <summary>
    /// ID của button (dùng cho JavaScript)
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Kích thước button
    /// </summary>
    public ButtonSize Size { get; set; } = ButtonSize.Default;

    /// <summary>
    /// Vô hiệu hóa button
    /// </summary>
    public bool IsDisabled { get; set; }

    /// <summary>
    /// Hiển thị loading spinner
    /// </summary>
    public bool ShowLoading { get; set; }

    /// <summary>
    /// Data attributes (key-value pairs cho data-* attributes)
    /// </summary>
    public Dictionary<string, string> DataAttributes { get; set; } = [];

    /// <summary>
    /// JavaScript onclick handler
    /// </summary>
    public string? OnClick { get; set; }

    /// <summary>
    /// Thứ tự hiển thị
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Constructor mặc định
    /// </summary>
    public ActionButtonModel()
    {
    }

    /// <summary>
    /// Constructor với các tham số cơ bản
    /// </summary>
    public ActionButtonModel(string text, string? url = null, string? icon = null, string cssClass = "btn-primary")
    {
        Text = text;
        Url = url;
        Icon = icon;
        CssClass = cssClass;
    }

    /// <summary>
    /// Tạo button "Thêm mới"
    /// </summary>
    public static ActionButtonModel Create(string url, string text = "Thêm mới") =>
        new(text, url, "fas fa-plus", "btn-primary");

    /// <summary>
    /// Tạo button "Sửa"
    /// </summary>
    public static ActionButtonModel Edit(string url, string text = "Sửa") =>
        new(text, url, "fas fa-edit", "btn-warning");

    /// <summary>
    /// Tạo button "Xóa"
    /// </summary>
    public static ActionButtonModel Delete(string? url = null, string text = "Xóa") =>
        new(text, url, "fas fa-trash", "btn-danger") { Type = ButtonType.Button };

    /// <summary>
    /// Tạo button "Quay lại"
    /// </summary>
    public static ActionButtonModel Back(string url, string text = "Quay lại") =>
        new(text, url, "fas fa-arrow-left", "btn-secondary");

    /// <summary>
    /// Tạo button "Lưu"
    /// </summary>
    public static ActionButtonModel Save(string text = "Lưu") =>
        new(text, null, "fas fa-save", "btn-success") { Type = ButtonType.Submit };

    /// <summary>
    /// Tạo button "Xuất Excel"
    /// </summary>
    public static ActionButtonModel ExportExcel(string url, string text = "Xuất Excel") =>
        new(text, url, "fas fa-file-excel", "btn-success");

    /// <summary>
    /// Tạo button "Xem chi tiết"
    /// </summary>
    public static ActionButtonModel Details(string url, string text = "Chi tiết") =>
        new(text, url, "fas fa-eye", "btn-info");
}

/// <summary>
/// Enum định nghĩa kiểu button
/// </summary>
public enum ButtonType
{
    /// <summary>
    /// Link (thẻ a)
    /// </summary>
    Link,

    /// <summary>
    /// Button thường
    /// </summary>
    Button,

    /// <summary>
    /// Submit button (trong form)
    /// </summary>
    Submit,

    /// <summary>
    /// Reset button (trong form)
    /// </summary>
    Reset
}

/// <summary>
/// Enum định nghĩa kích thước button
/// </summary>
public enum ButtonSize
{
    /// <summary>
    /// Kích thước nhỏ (btn-sm)
    /// </summary>
    Small,

    /// <summary>
    /// Kích thước mặc định
    /// </summary>
    Default,

    /// <summary>
    /// Kích thước lớn (btn-lg)
    /// </summary>
    Large
}
