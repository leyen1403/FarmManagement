using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropCareLogs.Queries.GetCropCareLogsByCropId;

public class GetCropCareLogsByCropIdHandler : IQueryHandler<GetCropCareLogsByCropIdQuery, IEnumerable<CropCareLogDto>>
{
    private readonly ICropCareLogService _service;

    public GetCropCareLogsByCropIdHandler(ICropCareLogService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<CropCareLogDto>> Handle(GetCropCareLogsByCropIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByCropIdAsync(request.CropId);
    }
}
