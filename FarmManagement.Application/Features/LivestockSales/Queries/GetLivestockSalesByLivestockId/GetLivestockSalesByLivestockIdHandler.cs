using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockSales.Queries.GetLivestockSalesByLivestockId;

public class GetLivestockSalesByLivestockIdHandler : IQueryHandler<GetLivestockSalesByLivestockIdQuery, List<LivestockSaleDto>>
{
    private readonly ILivestockSaleService _service;

    public GetLivestockSalesByLivestockIdHandler(ILivestockSaleService service) => _service = service;

    public async Task<List<LivestockSaleDto>> Handle(GetLivestockSalesByLivestockIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByLivestockIdAsync(request.LivestockId);
    }
}
