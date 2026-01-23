using AutoMapper;
using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Application.Interfaces.Locations;
using FarmManagement.Application.Interfaces.Common;
using FarmManagement.Infrastructure.Persistence.DbContext;
using FarmManagement.Domain.Entities.Locations;
using FarmManagement.Domain.Entities.Common;
using FarmManagement.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.Infrastructure.Services.Locations;

public class LocationStatusService : ILocationStatusService
{
    private readonly FarmManagementDbContext _context;
    private readonly IMapper _mapper;
    private readonly IActivityLogService _activityLogService;

    public LocationStatusService(FarmManagementDbContext context, IMapper mapper, IActivityLogService activityLogService)
    {
        _context = context;
        _mapper = mapper;
        _activityLogService = activityLogService;
    }

    public async Task<IEnumerable<LocationStatusDto>> GetAllAsync()
    {
        var entities = await _context.LocationStatuses.ToListAsync();
        return _mapper.Map<IEnumerable<LocationStatusDto>>(entities);
    }

    public async Task<LocationStatusDto> GetByIdAsync(int id)
    {
        var entity = await _context.LocationStatuses.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            throw new NotFoundException($"LocationStatus {id} not found");

        return _mapper.Map<LocationStatusDto>(entity);
    }

    public async Task<LocationStatusDto> CreateAsync(LocationStatusDto dto)
    {
        if (await _context.LocationStatuses.AnyAsync(x => x.Code == dto.Code))
            throw new BusinessException("Code đã tồn tại");

        var entity = _mapper.Map<LocationStatus>(dto);
        _context.LocationStatuses.Add(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Create,
            ActivityEntityTypes.LocationStatus,
            entity.Id,
            entity.Name,
            $"Thêm trạng thái vị trí mới \"{entity.Name}\""
        );

        return _mapper.Map<LocationStatusDto>(entity);
    }

    public async Task UpdateAsync(LocationStatusDto dto)
    {
        var entity = await _context.LocationStatuses.FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (entity == null)
            throw new NotFoundException($"LocationStatus {dto.Id} not found");

        _mapper.Map(dto, entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Update,
            ActivityEntityTypes.LocationStatus,
            entity.Id,
            entity.Name,
            $"Cập nhật trạng thái vị trí \"{entity.Name}\""
        );
    }

    public async Task DeleteAsync(int id)
    {
        if (await _context.Locations.AnyAsync(x => x.LocationStatusId == id))
            throw new BusinessException("Không thể xóa status đang được sử dụng");

        var entity = await _context.LocationStatuses.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            return;

        var entityName = entity.Name;
        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Delete,
            ActivityEntityTypes.LocationStatus,
            id,
            entityName,
            $"Xóa trạng thái vị trí \"{entityName}\""
        );
    }
}

// Refactor repository logic into a separate repository class (not implemented here for brevity).