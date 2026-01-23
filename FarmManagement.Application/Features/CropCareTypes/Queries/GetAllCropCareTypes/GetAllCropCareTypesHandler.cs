using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropCareTypes.Queries.GetAllCropCareTypes;

public class GetAllCropCareTypesHandler : IQueryHandler<GetAllCropCareTypesQuery, IEnumerable<CropCareTypeDto>>
{
    private readonly ICropCareTypeService _service;

    public GetAllCropCareTypesHandler(ICropCareTypeService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<CropCareTypeDto>> Handle(GetAllCropCareTypesQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAllAsync(request.IncludeInactive);
    }
}
