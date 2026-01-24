using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;
using FarmManagement.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using FarmManagement.Domain.Entities.Livestocks;

namespace FarmManagement.Infrastructure.Services.Livestocks;

public class LivestockPriceService : ILivestockPriceService
{
    private readonly FarmManagementDbContext _db;
    public LivestockPriceService(FarmManagementDbContext db) => _db = db;

    public async Task<List<LivestockPriceDto>> GetByLivestockIdAsync(int livestockId)
    {
        var items = await _db.Set<LivestockPrice>()
        .Where(x => x.LivestockId == livestockId)
        .OrderByDescending(x => x.EffectiveFrom)
        .ToListAsync();

        return items.Select(x => new LivestockPriceDto
        {
            Id = x.Id,
            LivestockId = x.LivestockId,
            Gender = x.Gender,
            UnitPrice = x.UnitPrice,
            EffectiveFrom = x.EffectiveFrom,
            EffectiveTo = x.EffectiveTo,
            IsActive = x.IsActive,
            Note = x.Note,
            CreatedDate = x.CreatedDate
        }).ToList();
    }

    public async Task<LivestockPriceDto?> GetByIdAsync(int id)
    {
        var x = await _db.Set<LivestockPrice>().FindAsync(id);
        if (x == null)
            return null;
        return new LivestockPriceDto
        {
            Id = x.Id,
            LivestockId = x.LivestockId,
            Gender = x.Gender,
            UnitPrice = x.UnitPrice,
            EffectiveFrom = x.EffectiveFrom,
            EffectiveTo = x.EffectiveTo,
            IsActive = x.IsActive,
            Note = x.Note,
            CreatedDate = x.CreatedDate
        };
    }

    public async Task<LivestockPriceDto?> GetActivePriceAsync(int livestockId, GenderType gender, DateTime atDate)
    {
        var price = await _db.Set<LivestockPrice>()
        .Where(x => x.LivestockId == livestockId && x.IsActive && x.Gender == gender)
        .Where(x => x.EffectiveFrom <= atDate && (x.EffectiveTo == null || x.EffectiveTo >= atDate))
        .OrderByDescending(x => x.EffectiveFrom)
        .FirstOrDefaultAsync();

        if (price == null)
            return null;
        return new LivestockPriceDto
        {
            Id = price.Id,
            LivestockId = price.LivestockId,
            Gender = price.Gender,
            UnitPrice = price.UnitPrice,
            EffectiveFrom = price.EffectiveFrom,
            EffectiveTo = price.EffectiveTo,
            IsActive = price.IsActive,
            Note = price.Note,
            CreatedDate = price.CreatedDate
        };
    }

    public async Task<int> CreateAsync(LivestockPriceDto dto)
    {
        var entity = new LivestockPrice
        {
            LivestockId = dto.LivestockId,
            Gender = dto.Gender,
            UnitPrice = dto.UnitPrice,
            EffectiveFrom = dto.EffectiveFrom,
            EffectiveTo = dto.EffectiveTo,
            IsActive = dto.IsActive,
            Note = dto.Note
        };
        _db.Set<LivestockPrice>().Add(entity);
        await _db.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(int id, LivestockPriceDto dto)
    {
        var entity = await _db.Set<LivestockPrice>().FindAsync(id);
        if (entity == null)
            return;
        entity.Gender = dto.Gender;
        entity.UnitPrice = dto.UnitPrice;
        entity.EffectiveFrom = dto.EffectiveFrom;
        entity.EffectiveTo = dto.EffectiveTo;
        entity.IsActive = dto.IsActive;
        entity.Note = dto.Note;
        entity.UpdatedDate = DateTime.UtcNow;
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _db.Set<LivestockPrice>().FindAsync(id);
        if (entity == null)
            return;
        _db.Set<LivestockPrice>().Remove(entity);
        await _db.SaveChangesAsync();
    }
}
