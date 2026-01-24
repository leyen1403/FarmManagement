// ***********************************************************************
// File: ILivestockHealthStatusService.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa interface cho các phương thức quản lý trạng thái sức khỏe vật nuôi.
// ***********************************************************************

using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Interfaces.Livestocks;

/// <summary>
/// Interface cung cấp các phương thức quản lý trạng thái sức khỏe vật nuôi (LivestockHealthStatus).
/// </summary>
public interface ILivestockHealthStatusService
{
    /// <summary>
    /// Lấy tất cả trạng thái sức khỏe vật nuôi.
    /// </summary>
    /// <returns>Danh sách các LivestockHealthStatusDto.</returns>
    Task<List<LivestockHealthStatusDto>> GetAllAsync();

    /// <summary>
    /// Lấy thông tin trạng thái sức khỏe theo ID.
    /// </summary>
    /// <param name="id">ID của trạng thái sức khỏe.</param>
    /// <returns>LivestockHealthStatusDto tương ứng hoặc null.</returns>
    Task<LivestockHealthStatusDto?> GetByIdAsync(int id);

    /// <summary>
    /// Tạo mới trạng thái sức khỏe vật nuôi.
    /// </summary>
    /// <param name="dto">Dữ liệu tạo mới.</param>
    /// <returns>ID của trạng thái sức khỏe vừa tạo.</returns>
    Task<int> CreateAsync(CreateLivestockHealthStatusDto dto);

    /// <summary>
    /// Cập nhật trạng thái sức khỏe vật nuôi.
    /// </summary>
    /// <param name="id">ID của trạng thái sức khỏe.</param>
    /// <param name="dto">Dữ liệu cập nhật.</param>
    Task UpdateAsync(int id, UpdateLivestockHealthStatusDto dto);

    /// <summary>
    /// Xóa trạng thái sức khỏe vật nuôi.
    /// </summary>
    /// <param name="id">ID của trạng thái sức khỏe.</param>
    Task DeleteAsync(int id);
}
