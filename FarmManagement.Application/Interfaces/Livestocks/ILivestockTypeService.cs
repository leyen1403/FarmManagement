// ***********************************************************************
// File: ILivestockTypeService.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa interface cho các phương thức quản lý loại vật nuôi.
// ***********************************************************************

using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Interfaces.Livestocks;

/// <summary>
/// Interface cung cấp các phương thức quản lý loại vật nuôi (LivestockType).
/// </summary>
public interface ILivestockTypeService
{
    /// <summary>
    /// Lấy tất cả loại vật nuôi.
    /// </summary>
    /// <param name="includeInactive">Nếu true, bao gồm cả loại không hoạt động.</param>
    /// <returns>Danh sách các LivestockTypeDto.</returns>
    Task<List<LivestockTypeDto>> GetAllAsync(bool includeInactive = false);

    /// <summary>
    /// Lấy thông tin loại vật nuôi theo ID.
    /// </summary>
    /// <param name="id">ID của loại vật nuôi.</param>
    /// <returns>LivestockTypeDto tương ứng hoặc null.</returns>
    Task<LivestockTypeDto?> GetByIdAsync(int id);

    /// <summary>
    /// Tạo mới loại vật nuôi.
    /// </summary>
    /// <param name="dto">Dữ liệu tạo mới.</param>
    /// <returns>ID của loại vật nuôi vừa tạo.</returns>
    Task<int> CreateAsync(CreateLivestockTypeDto dto);

    /// <summary>
    /// Cập nhật loại vật nuôi.
    /// </summary>
    /// <param name="id">ID của loại vật nuôi.</param>
    /// <param name="dto">Dữ liệu cập nhật.</param>
    Task UpdateAsync(int id, UpdateLivestockTypeDto dto);

    /// <summary>
    /// Xóa loại vật nuôi.
    /// </summary>
    /// <param name="id">ID của loại vật nuôi.</param>
    Task DeleteAsync(int id);
}
