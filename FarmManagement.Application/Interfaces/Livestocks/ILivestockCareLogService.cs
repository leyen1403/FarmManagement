// ***********************************************************************
// File: ILivestockCareLogService.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa interface cho các phương thức quản lý nhật ký chăm sóc vật nuôi.
// ***********************************************************************

using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Interfaces.Livestocks;

/// <summary>
/// Interface cung cấp các phương thức quản lý nhật ký chăm sóc vật nuôi (LivestockCareLog).
/// </summary>
public interface ILivestockCareLogService
{
    /// <summary>
    /// Lấy tất cả nhật ký chăm sóc của một vật nuôi.
    /// </summary>
    /// <param name="livestockId">ID của vật nuôi.</param>
    /// <returns>Danh sách các LivestockCareLogDto.</returns>
    Task<List<LivestockCareLogDto>> GetByLivestockIdAsync(int livestockId);

    /// <summary>
    /// Lấy thông tin nhật ký chăm sóc theo ID.
    /// </summary>
    /// <param name="id">ID của nhật ký chăm sóc.</param>
    /// <returns>LivestockCareLogDto tương ứng hoặc null.</returns>
    Task<LivestockCareLogDto?> GetByIdAsync(int id);

    /// <summary>
    /// Tạo mới nhật ký chăm sóc vật nuôi.
    /// </summary>
    /// <param name="dto">Dữ liệu tạo mới.</param>
    /// <returns>ID của nhật ký chăm sóc vừa tạo.</returns>
    Task<int> CreateAsync(CreateLivestockCareLogDto dto);

    /// <summary>
    /// Cập nhật nhật ký chăm sóc vật nuôi.
    /// </summary>
    /// <param name="id">ID của nhật ký chăm sóc.</param>
    /// <param name="dto">Dữ liệu cập nhật.</param>
    Task UpdateAsync(int id, UpdateLivestockCareLogDto dto);

    /// <summary>
    /// Xóa nhật ký chăm sóc vật nuôi.
    /// </summary>
    /// <param name="id">ID của nhật ký chăm sóc.</param>
    Task DeleteAsync(int id);
}
