using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Interfaces.Crops;

/// <summary>
/// Interface quản lý loại chi phí.
/// </summary>
public interface ICostTypeService
{
    /// <summary>
    /// Lấy tất cả loại chi phí.
    /// </summary>
    Task<IEnumerable<CostTypeDto>> GetAllAsync(bool includeInactive = false);

    /// <summary>
    /// Lấy loại chi phí theo ID.
    /// </summary>
    Task<CostTypeDto?> GetByIdAsync(int id);

    /// <summary>
    /// Tạo mới loại chi phí.
    /// </summary>
    Task<int> CreateAsync(CreateCostTypeDto dto);

    /// <summary>
    /// Cập nhật loại chi phí.
    /// </summary>
    Task UpdateAsync(int id, UpdateCostTypeDto dto);

    /// <summary>
    /// Xóa loại chi phí.
    /// </summary>
    Task DeleteAsync(int id);
}
