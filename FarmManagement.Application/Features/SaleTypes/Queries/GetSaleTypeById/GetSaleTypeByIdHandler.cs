using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.SaleTypes.Queries.GetSaleTypeById;

public class GetSaleTypeByIdHandler : IQueryHandler<GetSaleTypeByIdQuery, SaleTypeDto?>
{
    private readonly ISaleTypeService _service;

    public GetSaleTypeByIdHandler(ISaleTypeService service) => _service = service;

    public async Task<SaleTypeDto?> Handle(GetSaleTypeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}
