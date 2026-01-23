using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;
using MediatR;

namespace FarmManagement.Application.Features.CropCareLogs.Commands.UpdateCropCareLog;

public class UpdateCropCareLogHandler : ICommandHandler<UpdateCropCareLogCommand>
{
    private readonly ICropCareLogService _service;

    public UpdateCropCareLogHandler(ICropCareLogService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(UpdateCropCareLogCommand request, CancellationToken cancellationToken)
    {
        var dto = new UpdateCropCareLogDto
        {
            Id = request.Id,
            CropId = request.CropId,
            CropCareTypeId = request.CropCareTypeId,
            CareDate = request.CareDate,
            Description = request.Description,
            Cost = request.Cost
        };

        await _service.UpdateAsync(request.Id, dto);
        return Unit.Value;
    }
}
