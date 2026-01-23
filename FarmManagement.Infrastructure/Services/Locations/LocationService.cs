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
    public class LocationService : ILocationService
    {
        private readonly FarmManagementDbContext _context;
        private readonly IMapper _mapper;
        private readonly IActivityLogService _activityLogService;

        public LocationService(FarmManagementDbContext context, IMapper mapper, IActivityLogService activityLogService)
        {
            _context = context;
            _mapper = mapper;
            _activityLogService = activityLogService;
        }

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

        public async Task<LocationDto> CreateAsync(LocationDto dto)
        {
            // Validate required fields
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

            // Validate ParentLocationId if provided
            if (dto.ParentLocationId.HasValue)
            {
                if (!await _context.Locations.AnyAsync(x => x.Id == dto.ParentLocationId.Value && !x.IsDeleted))
                    throw new BusinessException("Địa điểm cha không tồn tại");
            }

            // Validate date range
            if (dto.StartDate.HasValue && dto.EndDate.HasValue && dto.StartDate > dto.EndDate)
                throw new BusinessException("Ngày bắt đầu không thể lớn hơn ngày kết thúc");

            var location = _mapper.Map<Location>(dto);
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            // Log activity
            _activityLogService.LogActivity(
                ActivityActionTypes.Create,
                ActivityEntityTypes.Location,
                location.Id,
                location.Name,
                $"Thêm vị trí mới \"{location.Name}\""
            );

            // Reload with includes for response
            var created = await _context.Locations
                .Include(x => x.LocationType)
                .Include(x => x.LocationStatus)
                .FirstAsync(x => x.Id == location.Id);

            return _mapper.Map<LocationDto>(created);
        }

        public async Task UpdateAsync(LocationDto dto)
        {
            var location = await _context.Locations.FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);
            if (location == null)
                throw new NotFoundException("Không tìm thấy địa điểm");

            // Validate required fields
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

            // Validate ParentLocationId if provided
            if (dto.ParentLocationId.HasValue)
            {
                if (dto.ParentLocationId.Value == dto.Id)
                    throw new BusinessException("Địa điểm không thể là cha của chính nó");

                if (!await _context.Locations.AnyAsync(x => x.Id == dto.ParentLocationId.Value && !x.IsDeleted))
                    throw new BusinessException("Địa điểm cha không tồn tại");
            }

            // Validate date range
            if (dto.StartDate.HasValue && dto.EndDate.HasValue && dto.StartDate > dto.EndDate)
                throw new BusinessException("Ngày bắt đầu không thể lớn hơn ngày kết thúc");

            // Update all fields
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

            // Log activity
            _activityLogService.LogActivity(
                ActivityActionTypes.Update,
                ActivityEntityTypes.Location,
                location.Id,
                location.Name,
                $"Cập nhật vị trí \"{location.Name}\""
            );
        }

        public async Task DeleteAsync(int id)
        {
            var location = await _context.Locations.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (location == null)
                return;

            // Check if location has children
            if (await _context.Locations.AnyAsync(x => x.ParentLocationId == id && !x.IsDeleted))
                throw new BusinessException("Không thể xóa địa điểm có địa điểm con");

            var locationName = location.Name;

            location.IsDeleted = true;
            location.DeletedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            // Log activity
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