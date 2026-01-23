using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Features.CropPrices.Queries.GetAllCropPrices;

public record GetAllCropPricesQuery(
    int? CropId = null,
    bool IncludeInactive = false
) : IQuery<IEnumerable<CropPriceDto>>;
