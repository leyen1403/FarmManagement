using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Features.CropCareTypes.Queries.GetCropCareTypeById;

public record GetCropCareTypeByIdQuery(int Id) : IQuery<CropCareTypeDto?>;
