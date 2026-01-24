using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Exceptions;
using FarmManagement.Application.Interfaces.Livestocks;
using FarmManagement.Application.Interfaces.Common;
using FarmManagement.Domain.Entities.Livestocks;
using FarmManagement.Domain.Entities.Common;
using FarmManagement.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.Infrastructure.Services.Livestocks;

public class SaleTypeService : ISaleTypeService
{
    private readonly FarmManagementDbContext _context;
    private readonly IActivityLogService _activityLogService;

    public SaleTypeService(FarmManagementDbContext context, IActivityLogService activityLogService)
    {
        _context = context;
        _activityLogService = activityLogService;
    }

    public async Task<List<SaleTypeDto>> GetAllAsync()
    {
        var items = await _context.SaleTypes
            .OrderBy(x => x.Name)
         .ToListAsync();

        return items.Select(x => new SaleTypeDto
        {
            Id = x.Id,
            Code = x.Code,
            Name = x.Name,
            SaleMethod = x.SaleMethod
        }).ToList();
    }

    public async Task<SaleTypeDto?> GetByIdAsync(int id)
    {
        var entity = await _context.SaleTypes
      .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return null;

        return new SaleTypeDto
        {
            Id = entity.Id,
            Code = entity.Code,
            Name = entity.Name,
            SaleMethod = entity.SaleMethod
        };
    }

    public async Task<int> CreateAsync(CreateSaleTypeDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Code))
            throw new BusinessException("Mã loại bán không được để trống");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new BusinessException("Tên loại bán không được để trống");

        if (await _context.SaleTypes.AnyAsync(x => x.Code == dto.Code))
            throw new BusinessException("Mã loại bán đã tồn tại");

        var entity = new SaleType
        {
            Code = dto.Code,
            Name = dto.Name,
            SaleMethod = dto.SaleMethod
        };

        _context.SaleTypes.Add(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
         ActivityActionTypes.Create,
     nameof(SaleType),
         entity.Id,
    entity.Name,
            $"Thêm loại bán vật nuôi \"{entity.Name}\" ({entity.SaleMethod})"
        );

        return entity.Id;
    }

    public async Task UpdateAsync(int id, UpdateSaleTypeDto dto)
    {
        var entity = await _context.SaleTypes
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException("Loại bán không tồn tại");

        if (string.IsNullOrWhiteSpace(dto.Code))
            throw new BusinessException("Mã loại bán không được để trống");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new BusinessException("Tên loại bán không được để trống");

        if (await _context.SaleTypes.AnyAsync(x => x.Code == dto.Code && x.Id != id))
            throw new BusinessException("Mã loại bán đã tồn tại");

        entity.Code = dto.Code;
        entity.Name = dto.Name;
        entity.SaleMethod = dto.SaleMethod;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
              ActivityActionTypes.Update,
            nameof(SaleType),
              entity.Id,
     entity.Name,
    $"Cập nhật loại bán vật nuôi \"{entity.Name}\" ({entity.SaleMethod})"
          );
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.SaleTypes
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return;

        if (await _context.LivestockSales.AnyAsync(x => x.SaleTypeId == id))
            throw new BusinessException("Không thể xóa loại bán đang được sử dụng");

        var name = entity.Name;

        _context.SaleTypes.Remove(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
           ActivityActionTypes.Delete,
             nameof(SaleType),
            id,
          name,
              $"Xóa loại bán vật nuôi \"{name}\""
           );
    }
}
