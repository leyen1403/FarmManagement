using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.Crops.Queries.GetCropById;

public class GetCropByIdHandler : IQueryHandler<GetCropByIdQuery, CropDto?>
{
    private readonly ICropService _cropService;

    public GetCropByIdHandler(ICropService cropService)
    {
        _cropService = cropService;
    }

    public async Task<CropDto?> Handle(GetCropByIdQuery request, CancellationToken cancellationToken)
    {
        return await _cropService.GetByIdAsync(request.Id);
    }
}
