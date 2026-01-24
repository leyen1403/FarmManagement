using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockCareLogs.Queries.GetLivestockCareLogsByLivestockId;

public class GetLivestockCareLogsByLivestockIdHandler : IQueryHandler<GetLivestockCareLogsByLivestockIdQuery, List<LivestockCareLogDto>>
{
    private readonly ILivestockCareLogService _service;

    public GetLivestockCareLogsByLivestockIdHandler(ILivestockCareLogService service) => _service = service;

    public async Task<List<LivestockCareLogDto>> Handle(GetLivestockCareLogsByLivestockIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByLivestockIdAsync(request.LivestockId);
    }
}
