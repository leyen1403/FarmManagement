using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;
using MediatR;

namespace FarmManagement.Application.Features.LivestockStatuses.Commands.DeleteLivestockStatus;

public class DeleteLivestockStatusHandler : ICommandHandler<DeleteLivestockStatusCommand>
{
    private readonly ILivestockStatusService _service;

    public DeleteLivestockStatusHandler(ILivestockStatusService service) => _service = service;

    public async Task<Unit> Handle(DeleteLivestockStatusCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
