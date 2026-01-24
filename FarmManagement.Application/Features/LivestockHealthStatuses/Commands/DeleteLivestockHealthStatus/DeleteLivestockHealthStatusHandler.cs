using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;
using MediatR;

namespace FarmManagement.Application.Features.LivestockHealthStatuses.Commands.DeleteLivestockHealthStatus;

public class DeleteLivestockHealthStatusHandler : ICommandHandler<DeleteLivestockHealthStatusCommand>
{
    private readonly ILivestockHealthStatusService _service;

    public DeleteLivestockHealthStatusHandler(ILivestockHealthStatusService service) => _service = service;

    public async Task<Unit> Handle(DeleteLivestockHealthStatusCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
