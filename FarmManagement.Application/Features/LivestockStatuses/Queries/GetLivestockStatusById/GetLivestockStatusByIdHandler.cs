using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockStatuses.Queries.GetLivestockStatusById;

public class GetLivestockStatusByIdHandler : IQueryHandler<GetLivestockStatusByIdQuery, LivestockStatusDto?>
{
    private readonly ILivestockStatusService _service;

    public GetLivestockStatusByIdHandler(ILivestockStatusService service) => _service = service;

    public async Task<LivestockStatusDto?> Handle(GetLivestockStatusByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}
