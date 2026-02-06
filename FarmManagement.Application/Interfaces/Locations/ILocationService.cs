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
        /// <param name="activeOnly">
        /// Nếu <c>true</c>, chỉ lấy các Location đang hoạt động (LocationStatusId = 1).
        /// Nếu <c>false</c>, lấy tất cả các Location chưa bị xóa.
        /// </param>
        /// <returns>
        /// Một <see cref="Task{TResult}"/> đại diện cho thao tác bất đồng bộ, với kết quả là tập hợp các <see cref="LocationDto"/>.
        /// </returns>
        Task<IEnumerable<LocationDto>> GetAllAsync(bool activeOnly = false);

        /// <summary>
        /// Lấy Location theo ID.
        /// </summary>
        /// <param name="id">ID của Location.</param>
        /// <returns>
        /// Một <see cref="Task{TResult}"/> đại diện cho thao tác bất đồng bộ, với kết quả là <see cref="LocationDto"/> tương ứng.
        /// </returns>
        Task<LocationDto> GetByIdAsync(int id);

        /// <summary>
        /// Tạo mới một Location.
        /// </summary>
        /// <param name="dto">DTO của Location cần tạo.</param>
        /// <returns>
        /// Một <see cref="Task{TResult}"/> đại diện cho thao tác bất đồng bộ, với kết quả là Location vừa được tạo dưới dạng <see cref="LocationDto"/>.
        /// </returns>
        Task<LocationDto> CreateAsync(LocationDto dto);

        /// <summary>
        /// Cập nhật thông tin Location.
        /// </summary>
        /// <param name="dto">DTO của Location cần cập nhật.</param>
        /// <returns>
        /// Một <see cref="Task"/> đại diện cho thao tác bất đồng bộ.
        /// </returns>
        Task UpdateAsync(LocationDto dto);

        /// <summary>
        /// Xóa Location theo ID.
        /// </summary>
        /// <param name="id">ID của Location cần xóa.</param>
        /// <returns>
        /// Một <see cref="Task"/> đại diện cho thao tác bất đồng bộ.
        /// </returns>
        Task DeleteAsync(int id);
    }
}
