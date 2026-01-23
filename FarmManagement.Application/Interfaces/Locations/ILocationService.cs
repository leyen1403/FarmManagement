// ***********************************************************************
// File: ILocationService.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa interface cho các phương thức quản lý Location.
// ***********************************************************************

using FarmManagement.Application.DTOs.Locations;

namespace FarmManagement.Application.Interfaces.Locations
{
    /// <summary>
    /// Interface cung cấp các phương thức quản lý Location.
    /// </summary>
    public interface ILocationService
    {
        /// <summary>
        /// Lấy tất cả các Location.
        /// </summary>
        /// <param name="activeOnly">Nếu true, chỉ lấy các Location đang hoạt động (LocationStatusId = 1).</param>
        /// <returns>Tập hợp các <see cref="LocationDto"/>.</returns>
        Task<IEnumerable<LocationDto>> GetAllAsync(bool activeOnly = false);

        /// <summary>
        /// Lấy Location theo ID.
        /// </summary>
        /// <param name="id">ID của Location.</param>
        /// <returns><see cref="LocationDto"/> tương ứng.</returns>
        Task<LocationDto> GetByIdAsync(int id);

        /// <summary>
        /// Tạo mới một Location.
        /// </summary>
        /// <param name="dto">DTO của Location cần tạo.</param>
        /// <returns>Location vừa được tạo dưới dạng <see cref="LocationDto"/>.</returns>
        Task<LocationDto> CreateAsync(LocationDto dto);

        /// <summary>
        /// Cập nhật thông tin Location.
        /// </summary>
        /// <param name="dto">DTO của Location cần cập nhật.</param>
        Task UpdateAsync(LocationDto dto);

        /// <summary>
        /// Xóa Location theo ID.
        /// </summary>
        /// <param name="id">ID của Location cần xóa.</param>
     Task DeleteAsync(int id);
    }
}
