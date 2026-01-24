using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;
using MediatR;

namespace FarmManagement.Application.Features.LivestockStatuses.Commands.UpdateLivestockStatus;

public class UpdateLivestockStatusHandler : ICommandHandler<UpdateLivestockStatusCommand>
{
    private readonly ILivestockStatusService _service;

    public UpdateLivestockStatusHandler(ILivestockStatusService service) => _service = service;

    public async Task<Unit> Handle(UpdateLivestockStatusCommand request, CancellationToken cancellationToken)
    {
        await _service.UpdateAsync(request.Id, request.Dto);
        return Unit.Value;
    }
}
