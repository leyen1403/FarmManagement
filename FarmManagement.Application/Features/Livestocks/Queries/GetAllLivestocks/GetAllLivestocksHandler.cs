using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.Livestocks.Queries.GetAllLivestocks;

public class GetAllLivestocksHandler : IQueryHandler<GetAllLivestocksQuery, IEnumerable<LivestockDto>>
{
    private readonly ILivestockService _service;
    public GetAllLivestocksHandler(ILivestockService service) => _service = service;

    public async Task<IEnumerable<LivestockDto>> Handle(GetAllLivestocksQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAllAsync(request.LivestockTypeId, request.LivestockStatusId, request.LocationId);
    }
}
