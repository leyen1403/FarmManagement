// ***********************************************************************
// File: ICropPriceService.cs
// Project: FarmManagement.Application
// Mô tả: Interface cho các phương thức quản lý giá cây trồng.
// ***********************************************************************

using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Interfaces.Crops;

/// <summary>
/// Interface cung cấp các phương thức quản lý giá cây trồng.
/// </summary>
public interface ICropPriceService
{
    /// <summary>
    /// Lấy tất cả giá cây trồng.
    /// </summary>
    /// <param name="cropId">Lọc theo cây trồng (tùy chọn).</param>
    /// <param name="includeInactive">Bao gồm giá không hoạt động.</param>
    Task<IEnumerable<CropPriceDto>> GetAllAsync(int? cropId = null, bool includeInactive = false);

    /// <summary>
    /// Lấy giá cây trồng theo ID.
    /// </summary>
    Task<CropPriceDto?> GetByIdAsync(int id);

    /// <summary>
    /// Lấy giá hiện tại của cây trồng.
    /// </summary>
    Task<CropPriceDto?> GetCurrentPriceAsync(int cropId);

    /// <summary>
    /// Tạo mới giá cây trồng.
    /// </summary>
    Task<int> CreateAsync(CreateCropPriceDto dto);

    /// <summary>
    /// Cập nhật giá cây trồng.
    /// </summary>
    Task UpdateAsync(int id, UpdateCropPriceDto dto);

    /// <summary>
    /// Xóa giá cây trồng.
    /// </summary>
    Task DeleteAsync(int id);
}
