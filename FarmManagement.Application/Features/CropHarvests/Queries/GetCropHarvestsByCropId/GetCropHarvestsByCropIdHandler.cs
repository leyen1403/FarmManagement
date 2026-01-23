using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropHarvests.Queries.GetCropHarvestsByCropId;

public class GetCropHarvestsByCropIdHandler : IQueryHandler<GetCropHarvestsByCropIdQuery, IEnumerable<CropHarvestDto>>
{
    private readonly ICropHarvestService _service;

    public GetCropHarvestsByCropIdHandler(ICropHarvestService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<CropHarvestDto>> Handle(GetCropHarvestsByCropIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByCropIdAsync(request.CropId);
    }
}
