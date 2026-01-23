using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Features.CropHarvests.Queries.GetCropHarvestById;

public record GetCropHarvestByIdQuery(int Id) : IQuery<CropHarvestDto?>;
