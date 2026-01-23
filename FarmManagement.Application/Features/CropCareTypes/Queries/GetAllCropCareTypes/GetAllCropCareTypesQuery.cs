using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Features.CropCareTypes.Queries.GetAllCropCareTypes;

public record GetAllCropCareTypesQuery(bool IncludeInactive = false) : IQuery<IEnumerable<CropCareTypeDto>>;
