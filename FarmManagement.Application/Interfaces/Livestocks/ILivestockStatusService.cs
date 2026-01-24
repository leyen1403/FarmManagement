// ***********************************************************************
// File: ILivestockStatusService.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa interface cho các phương thức quản lý trạng thái vật nuôi.
// ***********************************************************************

using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Interfaces.Livestocks;

/// <summary>
/// Interface cung cấp các phương thức quản lý trạng thái vật nuôi (LivestockStatus).
/// </summary>
public interface ILivestockStatusService
{
    /// <summary>
    /// Lấy tất cả trạng thái vật nuôi.
    /// </summary>
    /// <param name="includeInactive">Nếu true, bao gồm cả trạng thái không hoạt động.</param>
    /// <returns>Danh sách các LivestockStatusDto.</returns>
    Task<List<LivestockStatusDto>> GetAllAsync(bool includeInactive = false);

    /// <summary>
    /// Lấy thông tin trạng thái vật nuôi theo ID.
    /// </summary>
    /// <param name="id">ID của trạng thái vật nuôi.</param>
    /// <returns>LivestockStatusDto tương ứng hoặc null.</returns>
    Task<LivestockStatusDto?> GetByIdAsync(int id);

    /// <summary>
    /// Tạo mới trạng thái vật nuôi.
    /// </summary>
    /// <param name="dto">Dữ liệu tạo mới.</param>
    /// <returns>ID của trạng thái vật nuôi vừa tạo.</returns>
    Task<int> CreateAsync(CreateLivestockStatusDto dto);

    /// <summary>
    /// Cập nhật trạng thái vật nuôi.
    /// </summary>
    /// <param name="id">ID của trạng thái vật nuôi.</param>
    /// <param name="dto">Dữ liệu cập nhật.</param>
    Task UpdateAsync(int id, UpdateLivestockStatusDto dto);

    /// <summary>
    /// Xóa trạng thái vật nuôi.
    /// </summary>
    /// <param name="id">ID của trạng thái vật nuôi.</param>
    Task DeleteAsync(int id);
}
