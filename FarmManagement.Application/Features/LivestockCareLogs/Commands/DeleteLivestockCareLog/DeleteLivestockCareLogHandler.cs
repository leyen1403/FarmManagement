using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;
using MediatR;

namespace FarmManagement.Application.Features.LivestockCareLogs.Commands.DeleteLivestockCareLog;

public class DeleteLivestockCareLogHandler : ICommandHandler<DeleteLivestockCareLogCommand>
{
    private readonly ILivestockCareLogService _service;

    public DeleteLivestockCareLogHandler(ILivestockCareLogService service) => _service = service;

    public async Task<Unit> Handle(DeleteLivestockCareLogCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
