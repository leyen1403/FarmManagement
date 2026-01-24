using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockStatuses.Queries.GetAllLivestockStatuses;

public class GetAllLivestockStatusesHandler : IQueryHandler<GetAllLivestockStatusesQuery, List<LivestockStatusDto>>
{
    private readonly ILivestockStatusService _service;

    public GetAllLivestockStatusesHandler(ILivestockStatusService service) => _service = service;

    public async Task<List<LivestockStatusDto>> Handle(GetAllLivestockStatusesQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAllAsync(request.IncludeInactive);
    }
}
