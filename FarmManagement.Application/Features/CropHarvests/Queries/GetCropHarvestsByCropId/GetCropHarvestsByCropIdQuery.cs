using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Features.CropHarvests.Queries.GetCropHarvestsByCropId;

public record GetCropHarvestsByCropIdQuery(int CropId) : IQuery<IEnumerable<CropHarvestDto>>;
