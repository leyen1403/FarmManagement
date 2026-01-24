using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Exceptions;
using FarmManagement.Application.Interfaces.Livestocks;
using FarmManagement.Application.Interfaces.Common;
using FarmManagement.Domain.Entities.Livestocks;
using FarmManagement.Domain.Entities.Common;
using FarmManagement.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.Infrastructure.Services.Livestocks;

public class LivestockCareTypeService : ILivestockCareTypeService
{
    private readonly FarmManagementDbContext _context;
    private readonly IActivityLogService _activityLogService;

    public LivestockCareTypeService(FarmManagementDbContext context, IActivityLogService activityLogService)
    {
        _context = context;
        _activityLogService = activityLogService;
    }

    public async Task<List<LivestockCareTypeDto>> GetAllAsync()
    {
        var items = await _context.LivestockCareTypes
        .OrderBy(x => x.Name)
               .ToListAsync();

        return items.Select(x => new LivestockCareTypeDto
        {
            Id = x.Id,
            Code = x.Code,
            Name = x.Name,
            Description = null // Entity doesn't have Description
        }).ToList();
    }

    public async Task<LivestockCareTypeDto?> GetByIdAsync(int id)
    {
        var entity = await _context.LivestockCareTypes
   .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return null;

        return new LivestockCareTypeDto
        {
            Id = entity.Id,
            Code = entity.Code,
            Name = entity.Name,
            Description = null
        };
    }

    public async Task<int> CreateAsync(CreateLivestockCareTypeDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Code))
            throw new BusinessException("Mã loại chăm sóc không được để trống");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new BusinessException("Tên loại chăm sóc không được để trống");

        if (await _context.LivestockCareTypes.AnyAsync(x => x.Code == dto.Code))
            throw new BusinessException("Mã loại chăm sóc đã tồn tại");

        var entity = new LivestockCareType
        {
            Code = dto.Code,
            Name = dto.Name
        };

        _context.LivestockCareTypes.Add(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
    ActivityActionTypes.Create,
  nameof(LivestockCareType),
   entity.Id,
   entity.Name,
        $"Thêm loại chăm sóc vật nuôi \"{entity.Name}\""
   );

        return entity.Id;
    }

    public async Task UpdateAsync(int id, UpdateLivestockCareTypeDto dto)
    {
        var entity = await _context.LivestockCareTypes
         .FirstOrDefaultAsync(x => x.Id == id)
          ?? throw new NotFoundException("Loại chăm sóc vật nuôi không tồn tại");

        if (string.IsNullOrWhiteSpace(dto.Code))
            throw new BusinessException("Mã loại chăm sóc không được để trống");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new BusinessException("Tên loại chăm sóc không được để trống");

        if (await _context.LivestockCareTypes.AnyAsync(x => x.Code == dto.Code && x.Id != id))
            throw new BusinessException("Mã loại chăm sóc đã tồn tại");

        entity.Code = dto.Code;
        entity.Name = dto.Name;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
              ActivityActionTypes.Update,
             nameof(LivestockCareType),
                entity.Id,
             entity.Name,
                  $"Cập nhật loại chăm sóc vật nuôi \"{entity.Name}\""
          );
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.LivestockCareTypes
         .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return;

        if (await _context.LivestockCareLogs.AnyAsync(x => x.LivestockCareTypeId == id))
            throw new BusinessException("Không thể xóa loại chăm sóc đang được sử dụng");

        var name = entity.Name;

        _context.LivestockCareTypes.Remove(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
               ActivityActionTypes.Delete,
        nameof(LivestockCareType),
         id,
               name,
             $"Xóa loại chăm sóc vật nuôi \"{name}\""
      );
    }
}
