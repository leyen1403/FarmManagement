using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Interfaces.Services;

/// <summary>
/// Interface cho service xuất báo cáo PDF tổng quan về vật nuôi
/// </summary>
public interface ILivestockPdfExportService
{
    /// <summary>
    /// Xuất báo cáo chi tiết vật nuôi (tổng quan)
    /// </summary>
    /// <param name="livestock">Thông tin vật nuôi</param>
    /// <param name="careLogs">Nhật ký chăm sóc</param>
    /// <param name="healthLogs">Nhật ký sức khỏe</param>
    /// <param name="sales">Đơn hàng bán</param>
    /// <returns>File PDF dưới dạng byte array</returns>
    byte[] ExportLivestockDetails(
        LivestockDto livestock,
        IEnumerable<LivestockCareLogDto> careLogs,
        IEnumerable<LivestockHealthLogDto> healthLogs,
        IEnumerable<LivestockSaleDto> sales);
}
