using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropCosts.Queries.GetCropCostsByCropId;

public class GetCropCostsByCropIdHandler : IQueryHandler<GetCropCostsByCropIdQuery, IEnumerable<CropCostDto>>
{
    private readonly ICropCostService _service;

    public GetCropCostsByCropIdHandler(ICropCostService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<CropCostDto>> Handle(GetCropCostsByCropIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByCropIdAsync(request.CropId);
    }
}
