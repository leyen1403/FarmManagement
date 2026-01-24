// ***********************************************************************
// File: ILivestockSaleService.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa interface cho các phương thức quản lý đơn hàng bán vật nuôi.
// ***********************************************************************

using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Interfaces.Livestocks;

/// <summary>
/// Interface cung cấp các phương thức quản lý đơn hàng bán vật nuôi (LivestockSale).
/// </summary>
public interface ILivestockSaleService
{
    /// <summary>
    /// Lấy tất cả đơn hàng bán của một vật nuôi.
    /// </summary>
    /// <param name="livestockId">ID của vật nuôi.</param>
    /// <returns>Danh sách các LivestockSaleDto.</returns>
    Task<List<LivestockSaleDto>> GetByLivestockIdAsync(int livestockId);

    /// <summary>
    /// Lấy thông tin đơn hàng bán theo ID (bao gồm chi tiết).
    /// </summary>
    /// <param name="id">ID của đơn hàng.</param>
    /// <returns>LivestockSaleDto tương ứng hoặc null.</returns>
    Task<LivestockSaleDto?> GetByIdAsync(int id);

    /// <summary>
    /// Tạo mới đơn hàng bán vật nuôi (bao gồm chi tiết).
    /// </summary>
    /// <param name="dto">Dữ liệu tạo mới.</param>
    /// <returns>ID của đơn hàng vừa tạo.</returns>
    Task<int> CreateAsync(CreateLivestockSaleDto dto);

    /// <summary>
    /// Cập nhật đơn hàng bán vật nuôi (bao gồm chi tiết).
    /// </summary>
    /// <param name="id">ID của đơn hàng.</param>
    /// <param name="dto">Dữ liệu cập nhật.</param>
    Task UpdateAsync(int id, UpdateLivestockSaleDto dto);

    /// <summary>
    /// Xóa đơn hàng bán vật nuôi (bao gồm tất cả chi tiết).
    /// </summary>
    /// <param name="id">ID của đơn hàng.</param>
    Task DeleteAsync(int id);

    /// <summary>
    /// Thêm chi tiết vào đơn hàng.
    /// </summary>
    /// <param name="saleId">ID của đơn hàng.</param>
    /// <param name="dto">Dữ liệu chi tiết.</param>
    /// <returns>ID của chi tiết vừa tạo.</returns>
    Task<int> AddDetailAsync(int saleId, CreateLivestockSaleDetailDto dto);

    /// <summary>
    /// Cập nhật chi tiết đơn hàng.
    /// </summary>
    /// <param name="detailId">ID của chi tiết.</param>
    /// <param name="dto">Dữ liệu cập nhật.</param>
    Task UpdateDetailAsync(int detailId, UpdateLivestockSaleDetailDto dto);

    /// <summary>
    /// Xóa chi tiết đơn hàng.
    /// </summary>
    /// <param name="detailId">ID của chi tiết.</param>
    Task DeleteDetailAsync(int detailId);

    /// <summary>
    /// Tính lại tổng cho đơn hàng.
    /// </summary>
    /// <param name="saleId">ID của đơn hàng.</param>
    Task RecalculateTotalsAsync(int saleId);
}
