using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockCareTypes.Queries.GetLivestockCareTypeById;

public class GetLivestockCareTypeByIdHandler : IQueryHandler<GetLivestockCareTypeByIdQuery, LivestockCareTypeDto?>
{
    private readonly ILivestockCareTypeService _service;

    public GetLivestockCareTypeByIdHandler(ILivestockCareTypeService service) => _service = service;

    public async Task<LivestockCareTypeDto?> Handle(GetLivestockCareTypeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}
