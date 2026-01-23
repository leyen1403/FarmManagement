using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Interfaces.Crops;

/// <summary>
/// Interface quản lý loại chăm sóc cây trồng.
/// </summary>
public interface ICropCareTypeService
{
    /// <summary>
    /// Lấy tất cả loại chăm sóc.
    /// </summary>
    Task<IEnumerable<CropCareTypeDto>> GetAllAsync(bool includeInactive = false);

    /// <summary>
    /// Lấy loại chăm sóc theo ID.
    /// </summary>
    Task<CropCareTypeDto?> GetByIdAsync(int id);

    /// <summary>
    /// Tạo mới loại chăm sóc.
    /// </summary>
    Task<int> CreateAsync(CreateCropCareTypeDto dto);

    /// <summary>
    /// Cập nhật loại chăm sóc.
    /// </summary>
    Task UpdateAsync(int id, UpdateCropCareTypeDto dto);

    /// <summary>
    /// Xóa loại chăm sóc.
    /// </summary>
    Task DeleteAsync(int id);
}
