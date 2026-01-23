using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Interfaces.Crops;

/// <summary>
/// Interface quản lý nhật ký chăm sóc cây trồng.
/// </summary>
public interface ICropCareLogService
{
    /// <summary>
    /// Lấy tất cả nhật ký chăm sóc của một cây trồng.
    /// </summary>
    Task<IEnumerable<CropCareLogDto>> GetByCropIdAsync(int cropId);

    /// <summary>
    /// Lấy nhật ký chăm sóc theo ID.
    /// </summary>
    Task<CropCareLogDto?> GetByIdAsync(int id);

    /// <summary>
    /// Tạo mới nhật ký chăm sóc.
    /// </summary>
    Task<int> CreateAsync(CreateCropCareLogDto dto);

    /// <summary>
    /// Cập nhật nhật ký chăm sóc.
    /// </summary>
    Task UpdateAsync(int id, UpdateCropCareLogDto dto);

    /// <summary>
    /// Xóa nhật ký chăm sóc.
    /// </summary>
    Task DeleteAsync(int id);
}
