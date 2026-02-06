using AutoMapper;
using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Application.Interfaces.Locations;
using FarmManagement.Application.Interfaces.Common;
using FarmManagement.Application.Exceptions;
using FarmManagement.Domain.Entities.Locations;
using FarmManagement.Domain.Entities.Common;
using FarmManagement.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.Infrastructure.Services.Locations
{
    /// <summary>
    /// Service triển khai các chức năng quản lý thực thể Location.
    /// </summary>
    public class LocationService : ILocationService
    {
        private readonly FarmManagementDbContext _context;
        private readonly IMapper _mapper;
        private readonly IActivityLogService _activityLogService;

        /// <summary>
        /// Khởi tạo một instance mới của <see cref="LocationService"/>.
        /// </summary>
        /// <param name="context">DbContext quản lý dữ liệu.</param>
        /// <param name="mapper">AutoMapper để ánh xạ DTO và entity.</param>
        /// <param name="activityLogService">Dịch vụ ghi log hoạt động.</param>
        public LocationService(FarmManagementDbContext context, IMapper mapper, IActivityLogService activityLogService)
        {
            _context = context;
            _mapper = mapper;
            _activityLogService = activityLogService;
        }

        /// <summary>
        /// Lấy danh sách tất cả các Location, có thể lọc chỉ lấy các Location đang hoạt động.
        /// </summary>
        /// <param name="activeOnly">Nếu true, chỉ lấy các Location đang hoạt động (LocationStatusId = 1).</param>
        /// <returns>Danh sách <see cref="LocationDto"/>.</returns>
        public async Task<IEnumerable<LocationDto>> GetAllAsync(bool activeOnly = false)
        {
            var query = _context.Locations
                .Include(x => x.LocationType)
                .Include(x => x.LocationStatus)
                .Where(x => !x.IsDeleted);

            // Lọc chỉ các Location đang hoạt động (LocationStatusId = 1 là "Hoạt động")
            if (activeOnly)
            {
                query = query.Where(x => x.LocationStatusId == 1);
            }

            var locations = await query.ToListAsync();
            return _mapper.Map<IEnumerable<LocationDto>>(locations);
        }

        /// <summary>
        /// Lấy thông tin Location theo Id.
        /// </summary>
        /// <param name="id">Id của Location.</param>
        /// <returns><see cref="LocationDto"/> nếu tìm thấy.</returns>
        /// <exception cref="NotFoundException">Nếu không tìm thấy Location.</exception>
        public async Task<LocationDto> GetByIdAsync(int id)
        {
            var location = await _context.Locations
                .Include(x => x.LocationType)
                .Include(x => x.LocationStatus)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (location == null)
                throw new NotFoundException("Không tìm thấy địa điểm");

            return _mapper.Map<LocationDto>(location);
        }

        /// <summary>
        /// Tạo mới một Location.
        /// </summary>
        /// <param name="dto">DTO chứa thông tin Location cần tạo.</param>
        /// <returns>Location vừa được tạo dưới dạng <see cref="LocationDto"/>.</returns>
        /// <exception cref="BusinessException">Nếu dữ liệu không hợp lệ.</exception>
        public async Task<LocationDto> CreateAsync(LocationDto dto)
        {
            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new BusinessException("Tên địa điểm không được để trống");

            if (!await _context.LocationTypes.AnyAsync(x => x.Id == dto.LocationTypeId && !x.IsDeleted))
                throw new BusinessException("LocationType không tồn tại");

            if (!await _context.LocationStatuses.AnyAsync(x => x.Id == dto.LocationStatusId && !x.IsDeleted))
                throw new BusinessException("LocationStatus không tồn tại");

            if (string.IsNullOrWhiteSpace(dto.Description))
                throw new BusinessException("Description không được để trống");

            if (string.IsNullOrWhiteSpace(dto.Address))
                throw new BusinessException("Address không được để trống");

            // Kiểm tra ParentLocationId nếu có
            if (dto.ParentLocationId.HasValue)
            {
                if (!await _context.Locations.AnyAsync(x => x.Id == dto.ParentLocationId.Value && !x.IsDeleted))
                    throw new BusinessException("Địa điểm cha không tồn tại");
            }

            // Kiểm tra ngày bắt đầu và kết thúc
            if (dto.StartDate.HasValue && dto.EndDate.HasValue && dto.StartDate > dto.EndDate)
                throw new BusinessException("Ngày bắt đầu không thể lớn hơn ngày kết thúc");

            var location = _mapper.Map<Location>(dto);
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            // Ghi log hoạt động
            _activityLogService.LogActivity(
                ActivityActionTypes.Create,
                ActivityEntityTypes.Location,
                location.Id,
                location.Name,
                $"Thêm vị trí mới \"{location.Name}\""
            );

            // Lấy lại dữ liệu vừa tạo kèm include
            var created = await _context.Locations
                .Include(x => x.LocationType)
                .Include(x => x.LocationStatus)
                .FirstAsync(x => x.Id == location.Id);

            return _mapper.Map<LocationDto>(created);
        }

        /// <summary>
        /// Cập nhật thông tin Location.
        /// </summary>
        /// <param name="dto">DTO chứa thông tin Location cần cập nhật.</param>
        /// <returns>Task bất đồng bộ.</returns>
        /// <exception cref="NotFoundException">Nếu không tìm thấy Location.</exception>
        /// <exception cref="BusinessException">Nếu dữ liệu không hợp lệ.</exception>
        public async Task UpdateAsync(LocationDto dto)
        {
            var location = await _context.Locations.FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);
            if (location == null)
                throw new NotFoundException("Không tìm thấy địa điểm");

            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new BusinessException("Tên địa điểm không được để trống");

            if (!await _context.LocationTypes.AnyAsync(x => x.Id == dto.LocationTypeId && !x.IsDeleted))
                throw new BusinessException("LocationType không tồn tại");

            if (!await _context.LocationStatuses.AnyAsync(x => x.Id == dto.LocationStatusId && !x.IsDeleted))
                throw new BusinessException("LocationStatus không tồn tại");

            if (string.IsNullOrWhiteSpace(dto.Description))
                throw new BusinessException("Description không được để trống");

            if (string.IsNullOrWhiteSpace(dto.Address))
                throw new BusinessException("Address không được để trống");

            // Kiểm tra ParentLocationId nếu có
            if (dto.ParentLocationId.HasValue)
            {
                if (dto.ParentLocationId.Value == dto.Id)
                    throw new BusinessException("Địa điểm không thể là cha của chính nó");

                if (!await _context.Locations.AnyAsync(x => x.Id == dto.ParentLocationId.Value && !x.IsDeleted))
                    throw new BusinessException("Địa điểm cha không tồn tại");
            }

            // Kiểm tra ngày bắt đầu và kết thúc
            if (dto.StartDate.HasValue && dto.EndDate.HasValue && dto.StartDate > dto.EndDate)
                throw new BusinessException("Ngày bắt đầu không thể lớn hơn ngày kết thúc");

            // Cập nhật các trường
            location.Name = dto.Name;
            location.Description = dto.Description;
            location.Address = dto.Address;
            location.LocationTypeId = dto.LocationTypeId;
            location.LocationStatusId = dto.LocationStatusId;
            location.Area = dto.Area;
            location.Capacity = dto.Capacity;
            location.CapacityUnit = dto.CapacityUnit;
            location.StartDate = dto.StartDate;
            location.EndDate = dto.EndDate;
            location.ParentLocationId = dto.ParentLocationId;
            location.Note = dto.Note;

            await _context.SaveChangesAsync();

            // Ghi log hoạt động
            _activityLogService.LogActivity(
                ActivityActionTypes.Update,
                ActivityEntityTypes.Location,
                location.Id,
                location.Name,
                $"Cập nhật vị trí \"{location.Name}\""
            );
        }

        /// <summary>
        /// Xóa mềm một Location theo Id.
        /// </summary>
        /// <param name="id">Id của Location cần xóa.</param>
        /// <returns>Task bất đồng bộ.</returns>
        /// <exception cref="BusinessException">Nếu Location có địa điểm con.</exception>
        public async Task DeleteAsync(int id)
        {
            var location = await _context.Locations.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (location == null)
                return;

            // Kiểm tra nếu Location có địa điểm con
            if (await _context.Locations.AnyAsync(x => x.ParentLocationId == id && !x.IsDeleted))
                throw new BusinessException("Không thể xóa địa điểm có địa điểm con");

            var locationName = location.Name;

            location.IsDeleted = true;
            location.DeletedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            // Ghi log hoạt động
            _activityLogService.LogActivity(
                ActivityActionTypes.Delete,
                ActivityEntityTypes.Location,
                id,
                locationName,
                $"Xóa vị trí \"{locationName}\""
            );
        }
    }
}