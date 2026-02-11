using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Interfaces.Services;

/// <summary>
/// Interface cho service xuất hóa đơn bán hàng PDF
/// </summary>
public interface ILivestockInvoiceExportService
{
    /// <summary>
    /// Xuất hóa đơn bán hàng cho một đơn hàng cụ thể
    /// </summary>
    /// <param name="sale">Thông tin đơn hàng bán</param>
    /// <param name="livestock">Thông tin vật nuôi</param>
    /// <returns>File PDF hóa đơn dưới dạng byte array</returns>
    byte[] ExportSaleInvoice(LivestockSaleDto sale, LivestockDto livestock);
}
