// ***********************************************************************
// File: ICropService.cs
// Project: FarmManagement.Application
// Mô tả: Interface cho các phương thức quản lý cây trồng.
// ***********************************************************************

using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Interfaces.Crops;

/// <summary>
/// Interface cung cấp các phương thức quản lý cây trồng.
/// </summary>
public interface ICropService
{
    /// <summary>
    /// Lấy tất cả cây trồng.
    /// </summary>
    /// <param name="cropTypeId">Lọc theo loại cây trồng (tùy chọn).</param>
    /// <param name="cropStatusId">Lọc theo trạng thái (tùy chọn).</param>
    /// <param name="locationId">Lọc theo vị trí (tùy chọn).</param>
    Task<IEnumerable<CropDto>> GetAllAsync(int? cropTypeId = null, int? cropStatusId = null, int? locationId = null);

    /// <summary>
    /// Lấy cây trồng theo ID.
    /// </summary>
    Task<CropDto?> GetByIdAsync(int id);

    /// <summary>
    /// Tạo mới cây trồng.
    /// </summary>
    Task<int> CreateAsync(CreateCropDto dto);

    /// <summary>
    /// Cập nhật cây trồng.
    /// </summary>
    Task UpdateAsync(int id, UpdateCropDto dto);

    /// <summary>
    /// Xóa cây trồng.
    /// </summary>
    Task DeleteAsync(int id);
}
