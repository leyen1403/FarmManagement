using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CostTypes.Queries.GetCostTypeById;

public class GetCostTypeByIdHandler : IQueryHandler<GetCostTypeByIdQuery, CostTypeDto?>
{
    private readonly ICostTypeService _service;

    public GetCostTypeByIdHandler(ICostTypeService service)
    {
        _service = service;
    }

    public async Task<CostTypeDto?> Handle(GetCostTypeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}
