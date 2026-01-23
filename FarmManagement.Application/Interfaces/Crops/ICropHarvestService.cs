using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Interfaces.Crops;

/// <summary>
/// Interface quản lý thu hoạch cây trồng.
/// </summary>
public interface ICropHarvestService
{
    /// <summary>
    /// Lấy tất cả thu hoạch của một cây trồng.
    /// </summary>
    Task<IEnumerable<CropHarvestDto>> GetByCropIdAsync(int cropId);

    /// <summary>
    /// Lấy thu hoạch theo ID.
    /// </summary>
    Task<CropHarvestDto?> GetByIdAsync(int id);

    /// <summary>
    /// Tạo mới thu hoạch.
    /// </summary>
    Task<int> CreateAsync(CreateCropHarvestDto dto);

    /// <summary>
    /// Cập nhật thu hoạch.
    /// </summary>
    Task UpdateAsync(int id, UpdateCropHarvestDto dto);

    /// <summary>
    /// Xóa thu hoạch.
    /// </summary>
    Task DeleteAsync(int id);
}
