namespace FarmManagement.Web.Models.Components;

/// <summary>
/// Model đại diện cho một thẻ thống kê (Stats Card)
/// </summary>
public class StatsCardModel
{
    /// <summary>
    /// Giá trị hiển thị (số liệu chính)
    /// </summary>
    public string Value { get; set; } = "0";

    /// <summary>
    /// Nhãn/mô tả cho giá trị
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Icon hiển thị (CSS class, ví dụ: "fas fa-users")
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// CSS class cho màu sắc (ví dụ: "bg-primary", "bg-success", "text-danger")
    /// </summary>
    public string? ColorClass { get; set; }

    /// <summary>
    /// CSS class cho icon background
    /// </summary>
    public string? IconBackgroundClass { get; set; }

    /// <summary>
    /// Phần trăm thay đổi so với kỳ trước
    /// </summary>
    public decimal? ChangePercentage { get; set; }

    /// <summary>
    /// Xu hướng thay đổi (tăng/giảm)
    /// </summary>
    public TrendDirection Trend { get; set; } = TrendDirection.Neutral;

    /// <summary>
    /// Mô tả xu hướng (ví dụ: "so với tháng trước")
    /// </summary>
    public string? TrendDescription { get; set; }

    /// <summary>
    /// Link điều hướng khi click vào card
    /// </summary>
    public string? LinkUrl { get; set; }

    /// <summary>
    /// Text hiển thị cho link
    /// </summary>
    public string? LinkText { get; set; }

    /// <summary>
    /// CSS class tùy chỉnh cho card
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// Định dạng hiển thị giá trị (ví dụ: "N0", "C0", "P2")
    /// </summary>
    public string? ValueFormat { get; set; }

    /// <summary>
    /// Đơn vị của giá trị (ví dụ: "VNĐ", "kg", "con")
    /// </summary>
    public string? Unit { get; set; }
}

/// <summary>
/// Enum định nghĩa xu hướng thay đổi
/// </summary>
public enum TrendDirection
{
    /// <summary>
    /// Xu hướng tăng
    /// </summary>
    Up,

    /// <summary>
    /// Xu hướng giảm
    /// </summary>
    Down,

    /// <summary>
    /// Không thay đổi/trung lập
    /// </summary>
    Neutral
}
