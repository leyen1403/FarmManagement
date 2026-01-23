using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropPrices.Queries.GetCropPriceHistory;

public class GetCropPriceHistoryHandler : IQueryHandler<GetCropPriceHistoryQuery, IEnumerable<CropPriceDto>>
{
    private readonly ICropPriceService _cropPriceService;

    public GetCropPriceHistoryHandler(ICropPriceService cropPriceService)
    {
        _cropPriceService = cropPriceService;
    }

    public async Task<IEnumerable<CropPriceDto>> Handle(GetCropPriceHistoryQuery request, CancellationToken cancellationToken)
    {
        // Lấy tất cả giá của cây trồng bao gồm cả inactive để xem lịch sử
        return await _cropPriceService.GetAllAsync(request.CropId, includeInactive: true);
    }
}
