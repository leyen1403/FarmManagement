using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropCareLogs.Commands.CreateCropCareLog;

public class CreateCropCareLogHandler : ICommandHandler<CreateCropCareLogCommand, int>
{
    private readonly ICropCareLogService _service;

    public CreateCropCareLogHandler(ICropCareLogService service)
    {
        _service = service;
    }

    public async Task<int> Handle(CreateCropCareLogCommand request, CancellationToken cancellationToken)
    {
        var dto = new CreateCropCareLogDto
        {
            CropId = request.CropId,
            CropCareTypeId = request.CropCareTypeId,
            CareDate = request.CareDate,
            Description = request.Description,
            Cost = request.Cost
        };

        return await _service.CreateAsync(dto);
    }
}
