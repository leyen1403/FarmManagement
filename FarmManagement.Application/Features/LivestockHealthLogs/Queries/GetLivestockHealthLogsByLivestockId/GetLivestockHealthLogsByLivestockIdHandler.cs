using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockHealthLogs.Queries.GetLivestockHealthLogsByLivestockId;

public class GetLivestockHealthLogsByLivestockIdHandler : IQueryHandler<GetLivestockHealthLogsByLivestockIdQuery, List<LivestockHealthLogDto>>
{
    private readonly ILivestockHealthLogService _service;

    public GetLivestockHealthLogsByLivestockIdHandler(ILivestockHealthLogService service) => _service = service;

    public async Task<List<LivestockHealthLogDto>> Handle(GetLivestockHealthLogsByLivestockIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByLivestockIdAsync(request.LivestockId);
    }
}
