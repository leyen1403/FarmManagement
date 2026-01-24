using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockCareLogs.Queries.GetLivestockCareLogById;

public class GetLivestockCareLogByIdHandler : IQueryHandler<GetLivestockCareLogByIdQuery, LivestockCareLogDto?>
{
    private readonly ILivestockCareLogService _service;

    public GetLivestockCareLogByIdHandler(ILivestockCareLogService service) => _service = service;

    public async Task<LivestockCareLogDto?> Handle(GetLivestockCareLogByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}
