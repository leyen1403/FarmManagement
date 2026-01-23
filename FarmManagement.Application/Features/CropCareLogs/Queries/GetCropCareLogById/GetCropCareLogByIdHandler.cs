using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropCareLogs.Queries.GetCropCareLogById;

public class GetCropCareLogByIdHandler : IQueryHandler<GetCropCareLogByIdQuery, CropCareLogDto?>
{
    private readonly ICropCareLogService _service;

    public GetCropCareLogByIdHandler(ICropCareLogService service)
    {
        _service = service;
    }

    public async Task<CropCareLogDto?> Handle(GetCropCareLogByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}
