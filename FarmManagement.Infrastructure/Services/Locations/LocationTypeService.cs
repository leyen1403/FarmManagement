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

public class LocationTypeService : ILocationTypeService
{
    private readonly FarmManagementDbContext _context;
    private readonly IMapper _mapper;
    private readonly IActivityLogService _activityLogService;

    public LocationTypeService(FarmManagementDbContext context, IMapper mapper, IActivityLogService activityLogService)
    {
        _context = context;
        _mapper = mapper;
        _activityLogService = activityLogService;
    }

    public async Task<IEnumerable<LocationTypeDto>> GetAllAsync()
    {
        var entities = await _context.LocationTypes.ToListAsync();
        return _mapper.Map<IEnumerable<LocationTypeDto>>(entities);
    }

    public async Task<LocationTypeDto> GetByIdAsync(int id)
    {
        var entity = await _context.LocationTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            throw new NotFoundException($"LocationType {id} not found");

        return _mapper.Map<LocationTypeDto>(entity);
    }

    public async Task<LocationTypeDto> CreateAsync(LocationTypeDto dto)
    {
        if (await _context.LocationTypes.AnyAsync(x => x.Code == dto.Code))
            throw new BusinessException("Code đã tồn tại");

        var entity = _mapper.Map<LocationType>(dto);
        _context.LocationTypes.Add(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Create,
            ActivityEntityTypes.LocationType,
            entity.Id,
            entity.Name,
            $"Thêm loại vị trí mới \"{entity.Name}\""
        );

        return _mapper.Map<LocationTypeDto>(entity);
    }

    public async Task UpdateAsync(LocationTypeDto dto)
    {
        var entity = await _context.LocationTypes.FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (entity == null)
            throw new NotFoundException($"LocationType {dto.Id} not found");

        _mapper.Map(dto, entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Update,
            ActivityEntityTypes.LocationType,
            entity.Id,
            entity.Name,
            $"Cập nhật loại vị trí \"{entity.Name}\""
        );
    }

    public async Task DeleteAsync(int id)
    {
        if (await _context.Locations.AnyAsync(x => x.LocationTypeId == id))
            throw new BusinessException("Không thể xóa type đang được sử dụng");

        var entity = await _context.LocationTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            return;

        var entityName = entity.Name;
        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Delete,
            ActivityEntityTypes.LocationType,
            id,
            entityName,
            $"Xóa loại vị trí \"{entityName}\""
        );
    }
}