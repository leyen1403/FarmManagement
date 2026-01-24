// ***********************************************************************
// File: ISaleTypeService.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa interface cho các phương thức quản lý loại bán vật nuôi.
// ***********************************************************************

using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Interfaces.Livestocks;

/// <summary>
/// Interface cung cấp các phương thức quản lý loại bán vật nuôi (SaleType).
/// </summary>
public interface ISaleTypeService
{
    /// <summary>
    /// Lấy tất cả loại bán vật nuôi.
    /// </summary>
    /// <returns>Danh sách các SaleTypeDto.</returns>
    Task<List<SaleTypeDto>> GetAllAsync();

    /// <summary>
    /// Lấy thông tin loại bán vật nuôi theo ID.
    /// </summary>
    /// <param name="id">ID của loại bán.</param>
    /// <returns>SaleTypeDto tương ứng hoặc null.</returns>
    Task<SaleTypeDto?> GetByIdAsync(int id);

    /// <summary>
    /// Tạo mới loại bán vật nuôi.
    /// </summary>
    /// <param name="dto">Dữ liệu tạo mới.</param>
    /// <returns>ID của loại bán vừa tạo.</returns>
    Task<int> CreateAsync(CreateSaleTypeDto dto);

    /// <summary>
    /// Cập nhật loại bán vật nuôi.
    /// </summary>
    /// <param name="id">ID của loại bán.</param>
    /// <param name="dto">Dữ liệu cập nhật.</param>
    Task UpdateAsync(int id, UpdateSaleTypeDto dto);

    /// <summary>
    /// Xóa loại bán vật nuôi.
    /// </summary>
    /// <param name="id">ID của loại bán.</param>
    Task DeleteAsync(int id);
}
