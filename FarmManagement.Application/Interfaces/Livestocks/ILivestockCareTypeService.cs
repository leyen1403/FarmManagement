// ***********************************************************************
// File: ILivestockCareTypeService.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa interface cho các phương thức quản lý loại chăm sóc vật nuôi.
// ***********************************************************************

using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Interfaces.Livestocks;

/// <summary>
/// Interface cung cấp các phương thức quản lý loại chăm sóc vật nuôi (LivestockCareType).
/// </summary>
public interface ILivestockCareTypeService
{
    /// <summary>
    /// Lấy tất cả loại chăm sóc vật nuôi.
    /// </summary>
    /// <returns>Danh sách các LivestockCareTypeDto.</returns>
    Task<List<LivestockCareTypeDto>> GetAllAsync();

    /// <summary>
    /// Lấy thông tin loại chăm sóc vật nuôi theo ID.
    /// </summary>
    /// <param name="id">ID của loại chăm sóc.</param>
    /// <returns>LivestockCareTypeDto tương ứng hoặc null.</returns>
    Task<LivestockCareTypeDto?> GetByIdAsync(int id);

    /// <summary>
    /// Tạo mới loại chăm sóc vật nuôi.
    /// </summary>
    /// <param name="dto">Dữ liệu tạo mới.</param>
    /// <returns>ID của loại chăm sóc vừa tạo.</returns>
    Task<int> CreateAsync(CreateLivestockCareTypeDto dto);

    /// <summary>
    /// Cập nhật loại chăm sóc vật nuôi.
    /// </summary>
    /// <param name="id">ID của loại chăm sóc.</param>
    /// <param name="dto">Dữ liệu cập nhật.</param>
    Task UpdateAsync(int id, UpdateLivestockCareTypeDto dto);

    /// <summary>
    /// Xóa loại chăm sóc vật nuôi.
    /// </summary>
    /// <param name="id">ID của loại chăm sóc.</param>
    Task DeleteAsync(int id);
}
