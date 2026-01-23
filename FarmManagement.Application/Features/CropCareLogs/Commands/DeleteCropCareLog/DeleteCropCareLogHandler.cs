using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Crops;
using MediatR;

namespace FarmManagement.Application.Features.CropCareLogs.Commands.DeleteCropCareLog;

public class DeleteCropCareLogHandler : ICommandHandler<DeleteCropCareLogCommand>
{
    private readonly ICropCareLogService _service;

    public DeleteCropCareLogHandler(ICropCareLogService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(DeleteCropCareLogCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
