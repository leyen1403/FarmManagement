using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockHealthLogs.Queries.GetLivestockHealthLogById;

public class GetLivestockHealthLogByIdHandler : IQueryHandler<GetLivestockHealthLogByIdQuery, LivestockHealthLogDto?>
{
    private readonly ILivestockHealthLogService _service;

    public GetLivestockHealthLogByIdHandler(ILivestockHealthLogService service) => _service = service;

    public async Task<LivestockHealthLogDto?> Handle(GetLivestockHealthLogByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}
