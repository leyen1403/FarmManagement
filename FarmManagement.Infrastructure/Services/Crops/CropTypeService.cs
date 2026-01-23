using AutoMapper;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;
using FarmManagement.Application.Interfaces.Common;
using FarmManagement.Domain.Entities.Crops;
using FarmManagement.Domain.Entities.Common;
using FarmManagement.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Globalization;

namespace FarmManagement.Infrastructure.Services.Crops;

/// <summary>
/// Cung cấp các phương thức thao tác dữ liệu loại cây trồng (CropType) trong cơ sở dữ liệu.
/// </summary>
public class CropTypeService : ICropTypeService
{
    private readonly FarmManagementDbContext _context;
    private readonly IMapper _mapper;
    private readonly IActivityLogService _activityLogService;

    /// <summary>
    /// Khởi tạo CropTypeService với DbContext và AutoMapper.
    /// </summary>
    public CropTypeService(FarmManagementDbContext context, IMapper mapper, IActivityLogService activityLogService)
    {
        _context = context;
        _mapper = mapper;
        _activityLogService = activityLogService;
    }

    /// <summary>
    /// Lấy tất cả loại cây trồng, có thể bao gồm loại không hoạt động.
    /// </summary>
    public async Task<List<CropTypeDto>> GetAllAsync(bool includeInactive = false)
    {
        var query = _context.CropTypes
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        if (!includeInactive)
        {
            query = query.Where(x => x.IsActive);
        }

        var entities = await query
            .OrderBy(x => x.Name)
            .ToListAsync();

        return _mapper.Map<List<CropTypeDto>>(entities);
    }

    /// <summary>
    /// Lấy thông tin loại cây trồng theo ID.
    /// </summary>
    public async Task<CropTypeDto?> GetByIdAsync(int id)
    {
        var entity = await _context.CropTypes
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (entity == null)
        {
            return null;
        }

        return _mapper.Map<CropTypeDto>(entity);
    }

    /// <summary>
    /// Tạo mới một loại cây trồng.
    /// </summary>
    public async Task<int> CreateAsync(CreateCropTypeDto dto)
    {
        // Handle Code - auto generate if not provided
        var code = string.IsNullOrWhiteSpace(dto.Code)
            ? GenerateCode(dto.Name)
            : dto.Code.Trim().ToUpper();

        bool exists = await _context.CropTypes
            .AnyAsync(x => x.Code == code && !x.IsDeleted);

        if (exists)
        {
            throw new Exception("Mã loại cây trồng đã tồn tại");
        }

        var entity = _mapper.Map<CropType>(dto);
        entity.Code = code;
        entity.IsActive = true;

        _context.CropTypes.Add(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Create,
            ActivityEntityTypes.CropType,
            entity.Id,
            entity.Name,
            $"Thêm loại cây trồng mới \"{entity.Name}\""
        );

        return entity.Id;
    }

    /// <summary>
    /// Cập nhật thông tin loại cây trồng theo ID.
    /// </summary>
    public async Task UpdateAsync(int id, UpdateCropTypeDto dto)
    {
        var entity = await _context.CropTypes
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (entity == null)
        {
            throw new Exception("Không tìm thấy loại cây trồng");
        }

        // Update all fields (Code is not updatable)
        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Update,
            ActivityEntityTypes.CropType,
            entity.Id,
            entity.Name,
            $"Cập nhật loại cây trồng \"{entity.Name}\""
        );
    }

    /// <summary>
    /// Xóa loại cây trồng theo ID.
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        var entity = await _context.CropTypes
            .Include(x => x.Crops)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (entity == null)
        {
            throw new Exception("Không tìm thấy loại cây trồng");
        }

        if (entity.Crops.Any(x => !x.IsDeleted))
        {
            throw new Exception("Loại cây trồng đang được sử dụng và không thể xóa");
        }

        var entityName = entity.Name;
        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
            ActivityActionTypes.Delete,
            ActivityEntityTypes.CropType,
            id,
            entityName,
            $"Xóa loại cây trồng \"{entityName}\""
        );
    }

    /// <summary>
    /// Generate code from name if not provided
    /// </summary>
    private string GenerateCode(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return $"CT_{DateTime.UtcNow.Ticks}";
        }

        // Remove Vietnamese diacritics and special characters
        var baseName = RemoveDiacritics(name).ToUpper();
        var code = baseName.Replace(" ", "_");

        return code.Substring(0, Math.Min(code.Length, 20));
    }

    private static string RemoveDiacritics(string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
}