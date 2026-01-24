using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Exceptions;
using FarmManagement.Application.Interfaces.Livestocks;
using FarmManagement.Application.Interfaces.Common;
using FarmManagement.Domain.Entities.Livestocks;
using FarmManagement.Domain.Entities.Common;
using FarmManagement.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.Infrastructure.Services.Livestocks;

public class LivestockHealthStatusService : ILivestockHealthStatusService
{
    private readonly FarmManagementDbContext _context;
    private readonly IActivityLogService _activityLogService;

    public LivestockHealthStatusService(FarmManagementDbContext context, IActivityLogService activityLogService)
    {
        _context = context;
        _activityLogService = activityLogService;
    }

    public async Task<List<LivestockHealthStatusDto>> GetAllAsync()
    {
        var items = await _context.LivestockHealthStatuses
    .OrderBy(x => x.Name)
       .ToListAsync();

        return items.Select(x => new LivestockHealthStatusDto
        {
            Id = x.Id,
            Code = x.Code,
            Name = x.Name
        }).ToList();
    }

    public async Task<LivestockHealthStatusDto?> GetByIdAsync(int id)
    {
        var entity = await _context.LivestockHealthStatuses
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return null;

        return new LivestockHealthStatusDto
        {
            Id = entity.Id,
            Code = entity.Code,
            Name = entity.Name
        };
    }

    public async Task<int> CreateAsync(CreateLivestockHealthStatusDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Code))
            throw new BusinessException("Mã trạng thái sức khỏe không được để trống");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new BusinessException("Tên trạng thái sức khỏe không được để trống");

        if (await _context.LivestockHealthStatuses.AnyAsync(x => x.Code == dto.Code))
            throw new BusinessException("Mã trạng thái sức khỏe đã tồn tại");

        var entity = new LivestockHealthStatus
        {
            Code = dto.Code,
            Name = dto.Name
        };

        _context.LivestockHealthStatuses.Add(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
           ActivityActionTypes.Create,
     nameof(LivestockHealthStatus),
           entity.Id,
           entity.Name,
         $"Thêm trạng thái sức khỏe \"{entity.Name}\""
             );

        return entity.Id;
    }

    public async Task UpdateAsync(int id, UpdateLivestockHealthStatusDto dto)
    {
        var entity = await _context.LivestockHealthStatuses
 .FirstOrDefaultAsync(x => x.Id == id)
      ?? throw new NotFoundException("Trạng thái sức khỏe không tồn tại");

        if (string.IsNullOrWhiteSpace(dto.Code))
            throw new BusinessException("Mã trạng thái sức khỏe không được để trống");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new BusinessException("Tên trạng thái sức khỏe không được để trống");

        if (await _context.LivestockHealthStatuses.AnyAsync(x => x.Code == dto.Code && x.Id != id))
            throw new BusinessException("Mã trạng thái sức khỏe đã tồn tại");

        entity.Code = dto.Code;
        entity.Name = dto.Name;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
        ActivityActionTypes.Update,
   nameof(LivestockHealthStatus),
       entity.Id,
      entity.Name,
     $"Cập nhật trạng thái sức khỏe \"{entity.Name}\""
           );
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.LivestockHealthStatuses
          .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return;

        if (await _context.LivestockHealthLogs.AnyAsync(x => x.HealthStatusId == id))
            throw new BusinessException("Không thể xóa trạng thái sức khỏe đang được sử dụng");

        var name = entity.Name;

        _context.LivestockHealthStatuses.Remove(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
     ActivityActionTypes.Delete,
   nameof(LivestockHealthStatus),
            id,
 name,
       $"Xóa trạng thái sức khỏe \"{name}\""
  );
    }
}
