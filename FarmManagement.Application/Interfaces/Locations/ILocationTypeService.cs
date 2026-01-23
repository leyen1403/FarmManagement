// ***********************************************************************
// File: ILocationTypeService.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa interface cho các phương thức quản lý loại Location.
// ***********************************************************************

using FarmManagement.Application.DTOs.Locations;

namespace FarmManagement.Application.Interfaces.Locations
{
    /// <summary>
    /// Interface cung cấp các phương thức quản lý loại Location.
    /// </summary>
    public interface ILocationTypeService
    {
        /// <summary>
        /// Lấy tất cả loại Location.
        /// </summary>
        /// <returns>Tập hợp các <see cref="LocationTypeDto"/>.</returns>
        Task<IEnumerable<LocationTypeDto>> GetAllAsync();

        /// <summary>
        /// Lấy loại Location theo ID.
        /// </summary>
        /// <param name="id">ID của loại Location.</param>
        /// <returns><see cref="LocationTypeDto"/> tương ứng.</returns>
        Task<LocationTypeDto> GetByIdAsync(int id);

        /// <summary>
        /// Tạo mới một loại Location.
        /// </summary>
        /// <param name="dto">DTO của loại Location cần tạo.</param>
        /// <returns>Loại Location vừa tạo dưới dạng <see cref="LocationTypeDto"/>.</returns>
        Task<LocationTypeDto> CreateAsync(LocationTypeDto dto);

        /// <summary>
        /// Cập nhật thông tin loại Location.
        /// </summary>
        /// <param name="dto">DTO của loại Location cần cập nhật.</param>
        Task UpdateAsync(LocationTypeDto dto);

        /// <summary>
        /// Xóa loại Location theo ID.
        /// </summary>
        /// <param name="id">ID của loại Location cần xóa.</param>
        Task DeleteAsync(int id);
    }
}
