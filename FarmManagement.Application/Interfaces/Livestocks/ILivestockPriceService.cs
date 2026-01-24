using FarmManagement.Domain.Entities.Livestocks;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Interfaces.Livestocks;

public interface ILivestockPriceService
{
    Task<List<LivestockPriceDto>> GetByLivestockIdAsync(int livestockId);
    Task<LivestockPriceDto?> GetByIdAsync(int id);
    Task<LivestockPriceDto?> GetActivePriceAsync(int livestockId, GenderType gender, DateTime atDate);
    Task<int> CreateAsync(LivestockPriceDto dto);
    Task UpdateAsync(int id, LivestockPriceDto dto);
    Task DeleteAsync(int id);
}
