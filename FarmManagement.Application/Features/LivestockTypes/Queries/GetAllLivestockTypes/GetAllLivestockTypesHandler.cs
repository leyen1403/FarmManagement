using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockTypes.Queries.GetAllLivestockTypes;

public class GetAllLivestockTypesHandler : IQueryHandler<GetAllLivestockTypesQuery, List<LivestockTypeDto>>
{
    private readonly ILivestockTypeService _service;

    public GetAllLivestockTypesHandler(ILivestockTypeService service) => _service = service;

    public async Task<List<LivestockTypeDto>> Handle(GetAllLivestockTypesQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAllAsync(request.IncludeInactive);
    }
}
