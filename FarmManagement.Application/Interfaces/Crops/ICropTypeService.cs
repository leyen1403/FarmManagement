// ***********************************************************************
// File: ICropTypeService.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa interface cho các phương thức quản lý loại cây trồng (CropType).
// ***********************************************************************

using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Interfaces.Crops
{
    /// <summary>
    /// Interface cung cấp các phương thức quản lý loại cây trồng (CropType).
    /// </summary>
    public interface ICropTypeService
    {
        /// <summary>
        /// Lấy tất cả loại cây trồng, có thể bao gồm hoặc loại trừ loại không hoạt động.
        /// </summary>
        /// <param name="includeInactive">Nếu true, bao gồm cả loại cây trồng không hoạt động. Mặc định là false.</param>
        /// <returns>Danh sách các CropTypeDto.</returns>
        Task<List<CropTypeDto>> GetAllAsync(bool includeInactive = false);

        /// <summary>
        /// Lấy thông tin loại cây trồng theo ID.
        /// </summary>
        /// <param name="id">ID của loại cây trồng.</param>
        /// <returns>CropTypeDto tương ứng hoặc null nếu không tìm thấy.</returns>
        Task<CropTypeDto?> GetByIdAsync(int id);

        /// <summary>
        /// Tạo mới một loại cây trồng.
        /// </summary>
        /// <param name="dto">Dữ liệu loại cây trồng cần tạo.</param>
        /// <returns>ID của loại cây trồng vừa tạo.</returns>
        Task<int> CreateAsync(CreateCropTypeDto dto);

        /// <summary>
        /// Cập nhật thông tin loại cây trồng theo ID.
        /// </summary>
        /// <param name="id">ID của loại cây trồng cần cập nhật.</param>
        /// <param name="dto">Dữ liệu cập nhật.</param>
        Task UpdateAsync(int id, UpdateCropTypeDto dto);

        /// <summary>
        /// Xóa loại cây trồng theo ID.
        /// </summary>
        /// <param name="id">ID của loại cây trồng cần xóa.</param>
        Task DeleteAsync(int id);
    }
}
