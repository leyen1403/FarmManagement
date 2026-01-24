using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockHealthStatuses.Queries.GetAllLivestockHealthStatuses;

public class GetAllLivestockHealthStatusesHandler : IQueryHandler<GetAllLivestockHealthStatusesQuery, List<LivestockHealthStatusDto>>
{
    private readonly ILivestockHealthStatusService _service;

    public GetAllLivestockHealthStatusesHandler(ILivestockHealthStatusService service) => _service = service;

    public async Task<List<LivestockHealthStatusDto>> Handle(GetAllLivestockHealthStatusesQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAllAsync();
    }
}
