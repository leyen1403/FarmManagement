using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropPrices.Queries.GetCropPriceById;

public class GetCropPriceByIdHandler : IQueryHandler<GetCropPriceByIdQuery, CropPriceDto?>
{
    private readonly ICropPriceService _cropPriceService;

    public GetCropPriceByIdHandler(ICropPriceService cropPriceService)
    {
        _cropPriceService = cropPriceService;
    }

    public async Task<CropPriceDto?> Handle(GetCropPriceByIdQuery request, CancellationToken cancellationToken)
    {
        return await _cropPriceService.GetByIdAsync(request.Id);
    }
}
