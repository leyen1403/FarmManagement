using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.Crops.Queries.GetAllCrops;

public class GetAllCropsHandler : IQueryHandler<GetAllCropsQuery, IEnumerable<CropDto>>
{
    private readonly ICropService _cropService;

    public GetAllCropsHandler(ICropService cropService)
    {
        _cropService = cropService;
    }

    public async Task<IEnumerable<CropDto>> Handle(GetAllCropsQuery request, CancellationToken cancellationToken)
    {
        return await _cropService.GetAllAsync(request.CropTypeId, request.CropStatusId, request.LocationId);
    }
}
