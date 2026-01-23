namespace FarmManagement.Application.DTOs.Common;

/// <summary>
/// DTO hiển thị thông tin hoạt động.
/// </summary>
public class ActivityLogDto
{
    public int Id { get; set; }
    public string ActionType { get; set; } = null!;
    public string EntityType { get; set; } = null!;
    public int EntityId { get; set; }
    public string EntityName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime ActionDate { get; set; }
    public string? PerformedBy { get; set; }

    /// <summary>
    /// Tính toán thời gian tương đối (vd: "2 phút trước").
    /// </summary>
    public string RelativeTime
    {
        get
        {
            var diff = DateTime.UtcNow - ActionDate;

            if (diff.TotalMinutes < 1)
                return "Vừa xong";
            if (diff.TotalMinutes < 60)
                return $"{(int)diff.TotalMinutes} phút trước";
            if (diff.TotalHours < 24)
                return $"{(int)diff.TotalHours} giờ trước";
            if (diff.TotalDays < 7)
                return $"{(int)diff.TotalDays} ngày trước";
            if (diff.TotalDays < 30)
                return $"{(int)(diff.TotalDays / 7)} tuần trước";

            return ActionDate.ToString("dd/MM/yyyy");
        }
    }

    /// <summary>
    /// Icon Bootstrap tương ứng với loại hoạt động.
    /// </summary>
    public string ActionIcon => ActionType switch
    {
        "Create" => "bi-plus-circle",
        "Update" => "bi-pencil",
        "Delete" => "bi-trash",
        _ => "bi-circle"
    };

    /// <summary>
    /// CSS class màu tương ứng với loại hoạt động.
    /// </summary>
    public string ActionColorClass => ActionType switch
    {
        "Create" => "success",
        "Update" => "primary",
        "Delete" => "danger",
        _ => "secondary"
    };
}

/// <summary>
/// DTO để tạo activity log.
/// </summary>
public class CreateActivityLogDto
{
    public string ActionType { get; set; } = null!;
    public string EntityType { get; set; } = null!;
    public int EntityId { get; set; }
    public string EntityName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? PerformedBy { get; set; }
}
