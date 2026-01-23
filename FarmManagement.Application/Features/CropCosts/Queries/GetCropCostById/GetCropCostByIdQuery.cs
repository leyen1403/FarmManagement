using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Features.CropCosts.Queries.GetCropCostById;

public record GetCropCostByIdQuery(int Id) : IQuery<CropCostDto?>;
