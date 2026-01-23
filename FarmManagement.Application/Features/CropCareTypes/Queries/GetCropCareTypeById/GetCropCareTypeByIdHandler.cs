using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropCareTypes.Queries.GetCropCareTypeById;

public class GetCropCareTypeByIdHandler : IQueryHandler<GetCropCareTypeByIdQuery, CropCareTypeDto?>
{
    private readonly ICropCareTypeService _service;

    public GetCropCareTypeByIdHandler(ICropCareTypeService service)
    {
        _service = service;
    }

    public async Task<CropCareTypeDto?> Handle(GetCropCareTypeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}
