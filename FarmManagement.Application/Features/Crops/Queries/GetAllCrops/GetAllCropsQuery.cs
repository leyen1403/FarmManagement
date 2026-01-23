using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Features.Crops.Queries.GetAllCrops;

/// <summary>
/// Query lấy tất cả cây trồng.
/// </summary>
public record GetAllCropsQuery(
    int? CropTypeId = null,
    int? CropStatusId = null,
    int? LocationId = null
) : IQuery<IEnumerable<CropDto>>;
