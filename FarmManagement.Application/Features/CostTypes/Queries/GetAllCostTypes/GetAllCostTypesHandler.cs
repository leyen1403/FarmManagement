using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CostTypes.Queries.GetAllCostTypes;

public class GetAllCostTypesHandler : IQueryHandler<GetAllCostTypesQuery, IEnumerable<CostTypeDto>>
{
    private readonly ICostTypeService _service;

    public GetAllCostTypesHandler(ICostTypeService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<CostTypeDto>> Handle(GetAllCostTypesQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAllAsync(request.IncludeInactive);
    }
}
