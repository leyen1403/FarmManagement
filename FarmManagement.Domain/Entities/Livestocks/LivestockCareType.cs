namespace FarmManagement.Domain.Entities.Livestocks;

/// <summary>
/// Loại chăm sóc vật nuôi trong hệ thống.
/// </summary>
public class LivestockCareType
{
    /// <summary>
    /// Mã định danh của loại chăm sóc vật nuôi.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã loại chăm sóc vật nuôi.
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Tên loại chăm sóc vật nuôi.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Danh sách các nhật ký chăm sóc vật nuôi thuộc loại này.
    /// </summary>
    public ICollection<LivestockCareLog> LivestockCareLogs { get; set; } = new List<LivestockCareLog>();
}