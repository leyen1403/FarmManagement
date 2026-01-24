using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.SaleTypes.Queries.GetAllSaleTypes;

public class GetAllSaleTypesHandler : IQueryHandler<GetAllSaleTypesQuery, List<SaleTypeDto>>
{
    private readonly ISaleTypeService _service;

    public GetAllSaleTypesHandler(ISaleTypeService service) => _service = service;

    public async Task<List<SaleTypeDto>> Handle(GetAllSaleTypesQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAllAsync();
    }
}
