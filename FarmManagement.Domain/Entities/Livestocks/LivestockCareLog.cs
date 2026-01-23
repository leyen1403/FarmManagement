namespace FarmManagement.Domain.Entities.Livestocks;

/// <summary>
/// Nhật ký chăm sóc vật nuôi.
/// </summary>
public class LivestockCareLog
{
    /// <summary>
    /// Mã định danh của nhật ký chăm sóc.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã định danh vật nuôi.
    /// </summary>
    public int LivestockId { get; set; }

    /// <summary>
    /// Mã định danh loại chăm sóc vật nuôi.
    /// </summary>
    public int LivestockCareTypeId { get; set; }

    /// <summary>
    /// Ngày thực hiện chăm sóc.
    /// </summary>
    public DateTime CareDate { get; set; }

    /// <summary>
    /// Số lượng chăm sóc (nếu có).
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    /// Đơn vị đo lường (nếu có).
    /// </summary>
    public string? Unit { get; set; }

    /// <summary>
    /// Chi phí chăm sóc.
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// Ghi chú về hoạt động chăm sóc (nếu có).
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Vật nuôi liên quan đến nhật ký chăm sóc.
    /// </summary>
    public Livestock Livestock { get; set; } = null!;

    /// <summary>
    /// Loại chăm sóc vật nuôi liên quan.
    /// </summary>
    public LivestockCareType LivestockCareType { get; set; } = null!;
}