using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Interfaces.Crops;

/// <summary>
/// Interface quản lý chi phí cây trồng.
/// </summary>
public interface ICropCostService
{
    /// <summary>
    /// Lấy tất cả chi phí của một cây trồng.
    /// </summary>
    Task<IEnumerable<CropCostDto>> GetByCropIdAsync(int cropId);

    /// <summary>
    /// Lấy chi phí theo ID.
    /// </summary>
    Task<CropCostDto?> GetByIdAsync(int id);

    /// <summary>
    /// Tạo mới chi phí.
    /// </summary>
    Task<int> CreateAsync(CreateCropCostDto dto);

    /// <summary>
    /// Cập nhật chi phí.
    /// </summary>
    Task UpdateAsync(int id, UpdateCropCostDto dto);

    /// <summary>
    /// Xóa chi phí.
    /// </summary>
    Task DeleteAsync(int id);
}
