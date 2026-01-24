using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockCareTypes.Queries.GetAllLivestockCareTypes;

public class GetAllLivestockCareTypesHandler : IQueryHandler<GetAllLivestockCareTypesQuery, List<LivestockCareTypeDto>>
{
    private readonly ILivestockCareTypeService _service;

    public GetAllLivestockCareTypesHandler(ILivestockCareTypeService service) => _service = service;

    public async Task<List<LivestockCareTypeDto>> Handle(GetAllLivestockCareTypesQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAllAsync();
    }
}
