using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.Livestocks.Queries.GetLivestockById;

public class GetLivestockByIdHandler : IQueryHandler<GetLivestockByIdQuery, LivestockDto?>
{
    private readonly ILivestockService _service;
    public GetLivestockByIdHandler(ILivestockService service) => _service = service;

    public async Task<LivestockDto?> Handle(GetLivestockByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}
