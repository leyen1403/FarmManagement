// ***********************************************************************
// File: ILocationStatusService.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa interface cho các phương thức quản lý trạng thái Location.
// ***********************************************************************

using FarmManagement.Application.DTOs.Locations;

namespace FarmManagement.Application.Interfaces.Locations
{
    /// <summary>
    /// Interface cung cấp các phương thức quản lý trạng thái Location.
    /// </summary>
    public interface ILocationStatusService
    {
        /// <summary>
        /// Lấy tất cả trạng thái Location.
        /// </summary>
        /// <returns>Tập hợp các <see cref="LocationStatusDto"/>.</returns>
        Task<IEnumerable<LocationStatusDto>> GetAllAsync();

        /// <summary>
        /// Lấy trạng thái Location theo ID.
        /// </summary>
        /// <param name="id">ID của trạng thái Location.</param>
        /// <returns><see cref="LocationStatusDto"/> tương ứng.</returns>
        Task<LocationStatusDto> GetByIdAsync(int id);

        /// <summary>
        /// Tạo mới một trạng thái Location.
        /// </summary>
        /// <param name="dto">DTO của trạng thái Location cần tạo.</param>
        /// <returns>Trạng thái Location vừa tạo dưới dạng <see cref="LocationStatusDto"/>.</returns>
        Task<LocationStatusDto> CreateAsync(LocationStatusDto dto);

        /// <summary>
        /// Cập nhật thông tin trạng thái Location.
        /// </summary>
        /// <param name="dto">DTO của trạng thái Location cần cập nhật.</param>
        Task UpdateAsync(LocationStatusDto dto);

        /// <summary>
        /// Xóa trạng thái Location theo ID.
        /// </summary>
        /// <param name="id">ID của trạng thái Location cần xóa.</param>
        Task DeleteAsync(int id);
    }
}
