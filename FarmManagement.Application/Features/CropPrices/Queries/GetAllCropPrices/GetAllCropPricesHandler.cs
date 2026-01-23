using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropPrices.Queries.GetAllCropPrices;

public class GetAllCropPricesHandler : IQueryHandler<GetAllCropPricesQuery, IEnumerable<CropPriceDto>>
{
    private readonly ICropPriceService _cropPriceService;

    public GetAllCropPricesHandler(ICropPriceService cropPriceService)
    {
        _cropPriceService = cropPriceService;
    }

    public async Task<IEnumerable<CropPriceDto>> Handle(GetAllCropPricesQuery request, CancellationToken cancellationToken)
    {
        return await _cropPriceService.GetAllAsync(request.CropId, request.IncludeInactive);
    }
}
