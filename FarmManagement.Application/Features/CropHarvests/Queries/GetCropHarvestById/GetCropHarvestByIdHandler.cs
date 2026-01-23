using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropHarvests.Queries.GetCropHarvestById;

public class GetCropHarvestByIdHandler : IQueryHandler<GetCropHarvestByIdQuery, CropHarvestDto?>
{
    private readonly ICropHarvestService _service;

    public GetCropHarvestByIdHandler(ICropHarvestService service)
    {
        _service = service;
    }

    public async Task<CropHarvestDto?> Handle(GetCropHarvestByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}
