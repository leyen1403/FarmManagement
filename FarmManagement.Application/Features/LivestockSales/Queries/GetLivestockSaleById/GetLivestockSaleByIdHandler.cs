using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockSales.Queries.GetLivestockSaleById;

public class GetLivestockSaleByIdHandler : IQueryHandler<GetLivestockSaleByIdQuery, LivestockSaleDto?>
{
    private readonly ILivestockSaleService _service;

    public GetLivestockSaleByIdHandler(ILivestockSaleService service) => _service = service;

    public async Task<LivestockSaleDto?> Handle(GetLivestockSaleByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}
