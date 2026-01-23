namespace FarmManagement.Domain.Entities.Livestocks;

/// <summary>
/// Nhật ký sức khỏe của vật nuôi.
/// </summary>
public class LivestockHealthLog
{
    /// <summary>
    /// Mã định danh của nhật ký sức khỏe.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã định danh vật nuôi.
    /// </summary>
    public int LivestockId { get; set; }

    /// <summary>
    /// Mã định danh trạng thái sức khỏe.
    /// </summary>
    public int HealthStatusId { get; set; }

    /// <summary>
    /// Ngày kiểm tra sức khỏe.
    /// </summary>
    public DateTime CheckDate { get; set; }

    /// <summary>
    /// Triệu chứng của vật nuôi (nếu có).
    /// </summary>
    public string? Symptom { get; set; }

    /// <summary>
    /// Phương pháp điều trị (nếu có).
    /// </summary>
    public string? Treatment { get; set; }

    /// <summary>
    /// Chi phí thuốc men.
    /// </summary>
    public decimal MedicineCost { get; set; }

    /// <summary>
    /// Tên bác sĩ thú y (nếu có).
    /// </summary>
    public string? VetName { get; set; }

    /// <summary>
    /// Vật nuôi liên quan đến nhật ký sức khỏe.
    /// </summary>
    public Livestock Livestock { get; set; } = null!;

    /// <summary>
    /// Trạng thái sức khỏe liên quan.
    /// </summary>
    public LivestockHealthStatus HealthStatus { get; set; } = null!;
}