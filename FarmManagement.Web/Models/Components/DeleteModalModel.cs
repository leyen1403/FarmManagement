namespace FarmManagement.Web.Models.Components;

/// <summary>
/// Model cho modal xác nhận xóa
/// </summary>
public class DeleteModalModel
{
    /// <summary>
    /// ID của modal (dùng cho JavaScript)
    /// </summary>
    public string ModalId { get; set; } = "deleteModal";

    /// <summary>
    /// Tiêu đề modal
    /// </summary>
    public string Title { get; set; } = "Xác nhận xóa";

    /// <summary>
    /// Icon cho tiêu đề
    /// </summary>
    public string TitleIcon { get; set; } = "bi bi-exclamation-triangle text-danger";

    /// <summary>
    /// Nội dung thông báo xác nhận
    /// </summary>
    public string Message { get; set; } = "Bạn có chắc chắn muốn xóa mục này không?";

    /// <summary>
    /// Thông báo cảnh báo bổ sung
    /// </summary>
    public string? WarningMessage { get; set; } = "Hành động này không thể hoàn tác.";

    /// <summary>
    /// Hiển thị tên item cần xóa
    /// </summary>
    public bool ShowItemName { get; set; } = true;

    /// <summary>
    /// Label cho tên item (ví dụ: "Tên:", "Mã:")
    /// </summary>
    public string ItemNameLabel { get; set; } = "Tên:";

    /// <summary>
    /// URL action của form (POST)
    /// </summary>
    public string ActionUrl { get; set; } = string.Empty;

    /// <summary>
    /// Tên controller (dùng khi không có ActionUrl cố định)
    /// </summary>
    public string? ControllerName { get; set; }

    /// <summary>
    /// Tên action (mặc định: Delete)
    /// </summary>
    public string ActionName { get; set; } = "Delete";

    /// <summary>
    /// Text cho nút xóa
    /// </summary>
    public string DeleteButtonText { get; set; } = "Xóa";

    /// <summary>
    /// CSS class cho nút xóa
    /// </summary>
    public string DeleteButtonClass { get; set; } = "btn-danger";

    /// <summary>
    /// Icon cho nút xóa
    /// </summary>
    public string DeleteButtonIcon { get; set; } = "bi bi-trash";

    /// <summary>
    /// Text cho nút hủy
    /// </summary>
    public string CancelButtonText { get; set; } = "Hủy";

    /// <summary>
    /// Hiển thị loading khi submit
    /// </summary>
    public bool ShowLoadingOnSubmit { get; set; } = true;

    /// <summary>
    /// Kích thước modal (sm, lg, xl hoặc để trống cho default)
    /// </summary>
    public string ModalSize { get; set; } = "";

    /// <summary>
    /// CSS class tùy chỉnh cho modal
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// Có yêu cầu nhập lý do xóa không
    /// </summary>
    public bool RequireReason { get; set; } = false;

    /// <summary>
    /// Label cho trường lý do
    /// </summary>
    public string ReasonLabel { get; set; } = "Lý do xóa:";

    /// <summary>
    /// Placeholder cho trường lý do
    /// </summary>
    public string ReasonPlaceholder { get; set; } = "Nhập lý do xóa...";

    /// <summary>
    /// Constructor mặc định
    /// </summary>
    public DeleteModalModel() { }

    /// <summary>
    /// Constructor với controller name
    /// </summary>
    public DeleteModalModel(string controllerName)
    {
        ControllerName = controllerName;
    }

    /// <summary>
    /// Tạo model cho xóa entity cơ bản
    /// </summary>
    public static DeleteModalModel ForEntity(string entityName, string controllerName)
    {
        return new DeleteModalModel
        {
            ControllerName = controllerName,
            Title = $"Xác nhận xóa {entityName.ToLower()}",
            Message = $"Bạn có chắc chắn muốn xóa {entityName.ToLower()} này không?",
            ItemNameLabel = $"Tên {entityName.ToLower()}:"
        };
    }
}
