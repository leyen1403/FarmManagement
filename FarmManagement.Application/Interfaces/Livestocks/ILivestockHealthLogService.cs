// ***********************************************************************
// File: ILivestockHealthLogService.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa interface cho các phương thức quản lý nhật ký sức khỏe vật nuôi.
// ***********************************************************************

using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Interfaces.Livestocks;

/// <summary>
/// Interface cung cấp các phương thức quản lý nhật ký sức khỏe vật nuôi (LivestockHealthLog).
/// </summary>
public interface ILivestockHealthLogService
{
    /// <summary>
    /// Lấy tất cả nhật ký sức khỏe của một vật nuôi.
    /// </summary>
    /// <param name="livestockId">ID của vật nuôi.</param>
    /// <returns>Danh sách các LivestockHealthLogDto.</returns>
    Task<List<LivestockHealthLogDto>> GetByLivestockIdAsync(int livestockId);

    /// <summary>
    /// Lấy thông tin nhật ký sức khỏe theo ID.
    /// </summary>
    /// <param name="id">ID của nhật ký sức khỏe.</param>
    /// <returns>LivestockHealthLogDto tương ứng hoặc null.</returns>
    Task<LivestockHealthLogDto?> GetByIdAsync(int id);

    /// <summary>
    /// Tạo mới nhật ký sức khỏe vật nuôi.
    /// </summary>
    /// <param name="dto">Dữ liệu tạo mới.</param>
    /// <returns>ID của nhật ký sức khỏe vừa tạo.</returns>
    Task<int> CreateAsync(CreateLivestockHealthLogDto dto);

    /// <summary>
    /// Cập nhật nhật ký sức khỏe vật nuôi.
    /// </summary>
    /// <param name="id">ID của nhật ký sức khỏe.</param>
    /// <param name="dto">Dữ liệu cập nhật.</param>
    Task UpdateAsync(int id, UpdateLivestockHealthLogDto dto);

    /// <summary>
    /// Xóa nhật ký sức khỏe vật nuôi.
    /// </summary>
    /// <param name="id">ID của nhật ký sức khỏe.</param>
    Task DeleteAsync(int id);
}
