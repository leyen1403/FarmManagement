// ***********************************************************************
// File: ICropStatusService.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa interface cho các phương thức quản lý trạng thái cây trồng.
// ***********************************************************************

using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Interfaces.Crops
{
    /// <summary>
    /// Interface cung cấp các phương thức quản lý trạng thái cây trồng.
    /// </summary>
    public interface ICropStatusService
    {
        /// <summary>
        /// Lấy tất cả trạng thái cây trồng, có thể bao gồm hoặc loại trừ trạng thái không hoạt động.
        /// </summary>
        /// <param name="includeInactive">Nếu true, bao gồm cả trạng thái không hoạt động.</param>
        /// <returns>Danh sách các CropStatusDto.</returns>
        Task<IEnumerable<CropStatusDto>> GetAllAsync(bool includeInactive);

        /// <summary>
        /// Lấy thông tin trạng thái cây trồng theo ID.
        /// </summary>
        /// <param name="id">ID của trạng thái cây trồng.</param>
        /// <returns>CropStatusDto tương ứng.</returns>
        Task<CropStatusDto> GetByIdAsync(int id);

        /// <summary>
        /// Tạo mới một trạng thái cây trồng.
        /// </summary>
        /// <param name="dto">Dữ liệu trạng thái cây trồng cần tạo.</param>
        Task CreateAsync(CreateCropStatusDto dto);

        /// <summary>
        /// Cập nhật thông tin trạng thái cây trồng theo ID.
        /// </summary>
        /// <param name="id">ID của trạng thái cây trồng cần cập nhật.</param>
        /// <param name="dto">Dữ liệu cập nhật.</param>
        Task UpdateAsync(int id, UpdateCropStatusDto dto);

        /// <summary>
        /// Xóa trạng thái cây trồng theo ID.
        /// </summary>
        /// <param name="id">ID của trạng thái cây trồng cần xóa.</param>
        Task DeleteAsync(int id);
    }
}
