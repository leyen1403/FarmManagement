using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockHealthStatuses.Queries.GetLivestockHealthStatusById;

public class GetLivestockHealthStatusByIdHandler : IQueryHandler<GetLivestockHealthStatusByIdQuery, LivestockHealthStatusDto?>
{
    private readonly ILivestockHealthStatusService _service;

    public GetLivestockHealthStatusByIdHandler(ILivestockHealthStatusService service) => _service = service;

    public async Task<LivestockHealthStatusDto?> Handle(GetLivestockHealthStatusByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}
