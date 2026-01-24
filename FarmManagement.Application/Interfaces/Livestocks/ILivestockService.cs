using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Interfaces.Livestocks;

public interface ILivestockService
{
    Task<IEnumerable<LivestockDto>> GetAllAsync(int? livestockTypeId = null, int? livestockStatusId = null, int? locationId = null);
    Task<LivestockDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateLivestockDto dto);
    Task UpdateAsync(int id, UpdateLivestockDto dto);
    Task DeleteAsync(int id);
}
