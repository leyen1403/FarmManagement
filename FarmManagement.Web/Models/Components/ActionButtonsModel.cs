namespace FarmManagement.Web.Models.Components;

/// <summary>
/// Model cho nhóm các action buttons trong một row của table
/// </summary>
public class ActionButtonsModel
{
    /// <summary>
    /// ID của entity (dùng để tạo URL)
    /// </summary>
    public object? EntityId { get; set; }

    /// <summary>
    /// Tên entity (dùng cho delete modal)
    /// </summary>
    public string? EntityName { get; set; }

    /// <summary>
    /// Controller name
    /// </summary>
    public string ControllerName { get; set; } = string.Empty;

    /// <summary>
    /// Danh sách các action buttons
    /// </summary>
    public List<ActionButtonModel> Buttons { get; set; } = [];

    /// <summary>
    /// Hiển thị nút Edit
    /// </summary>
    public bool ShowEdit { get; set; } = true;

    /// <summary>
    /// Hiển thị nút Details
    /// </summary>
    public bool ShowDetails { get; set; } = true;

    /// <summary>
    /// Hiển thị nút Delete
    /// </summary>
    public bool ShowDelete { get; set; } = true;

    /// <summary>
    /// Kiểu hiển thị buttons
    /// </summary>
    public ActionButtonsDisplayMode DisplayMode { get; set; } = ActionButtonsDisplayMode.Buttons;

    /// <summary>
    /// Kích thước buttons
    /// </summary>
    public ButtonSize ButtonSize { get; set; } = ButtonSize.Small;

    /// <summary>
    /// Hiển thị text hay chỉ icon
    /// </summary>
    public bool ShowText { get; set; } = false;

    /// <summary>
    /// CSS class cho container
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// ID của delete modal (để trigger)
    /// </summary>
    public string DeleteModalId { get; set; } = "deleteModal";

    /// <summary>
    /// Custom URL cho Edit action
    /// </summary>
    public string? EditUrl { get; set; }

    /// <summary>
    /// Custom URL cho Details action
    /// </summary>
    public string? DetailsUrl { get; set; }

    /// <summary>
    /// Constructor mặc định
    /// </summary>
    public ActionButtonsModel() { }

    /// <summary>
    /// Constructor với các tham số cơ bản
    /// </summary>
    public ActionButtonsModel(object entityId, string controllerName, string? entityName = null)
    {
        EntityId = entityId;
        ControllerName = controllerName;
        EntityName = entityName;
    }

    /// <summary>
    /// Tạo model cơ bản cho CRUD operations
    /// </summary>
    public static ActionButtonsModel ForCrud(object entityId, string controllerName, string? entityName = null)
    {
        return new ActionButtonsModel
        {
            EntityId = entityId,
            ControllerName = controllerName,
            EntityName = entityName,
            ShowEdit = true,
            ShowDetails = true,
            ShowDelete = true,
            DisplayMode = ActionButtonsDisplayMode.Buttons,
            ButtonSize = ButtonSize.Small,
            ShowText = false
        };
    }

    /// <summary>
    /// Tạo model chỉ có Edit và Delete
    /// </summary>
    public static ActionButtonsModel ForEditDelete(object entityId, string controllerName, string? entityName = null)
    {
        return new ActionButtonsModel
        {
            EntityId = entityId,
            ControllerName = controllerName,
            EntityName = entityName,
            ShowEdit = true,
            ShowDetails = false,
            ShowDelete = true,
            DisplayMode = ActionButtonsDisplayMode.Buttons,
            ButtonSize = ButtonSize.Small
        };
    }

    /// <summary>
    /// Tạo model dạng dropdown
    /// </summary>
    public static ActionButtonsModel AsDropdown(object entityId, string controllerName, string? entityName = null)
    {
        return new ActionButtonsModel
        {
            EntityId = entityId,
            ControllerName = controllerName,
            EntityName = entityName,
            ShowEdit = true,
            ShowDetails = true,
            ShowDelete = true,
            DisplayMode = ActionButtonsDisplayMode.Dropdown
        };
    }
}

/// <summary>
/// Enum định nghĩa kiểu hiển thị action buttons
/// </summary>
public enum ActionButtonsDisplayMode
{
    /// <summary>
    /// Hiển thị dạng buttons inline
    /// </summary>
    Buttons,

    /// <summary>
    /// Hiển thị dạng button group
    /// </summary>
    ButtonGroup,

    /// <summary>
    /// Hiển thị dạng dropdown menu
    /// </summary>
    Dropdown,

    /// <summary>
    /// Hiển thị dạng icon only
    /// </summary>
    IconsOnly
}
