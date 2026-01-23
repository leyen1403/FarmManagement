using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropCosts.Queries.GetCropCostById;

public class GetCropCostByIdHandler : IQueryHandler<GetCropCostByIdQuery, CropCostDto?>
{
    private readonly ICropCostService _service;

    public GetCropCostByIdHandler(ICropCostService service)
    {
        _service = service;
    }

    public async Task<CropCostDto?> Handle(GetCropCostByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}
