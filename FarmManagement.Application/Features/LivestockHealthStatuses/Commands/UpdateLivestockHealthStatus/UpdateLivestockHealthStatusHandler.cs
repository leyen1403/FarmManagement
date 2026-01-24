using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;
using MediatR;

namespace FarmManagement.Application.Features.LivestockHealthStatuses.Commands.UpdateLivestockHealthStatus;

public class UpdateLivestockHealthStatusHandler : ICommandHandler<UpdateLivestockHealthStatusCommand>
{
    private readonly ILivestockHealthStatusService _service;

    public UpdateLivestockHealthStatusHandler(ILivestockHealthStatusService service) => _service = service;

    public async Task<Unit> Handle(UpdateLivestockHealthStatusCommand request, CancellationToken cancellationToken)
    {
        await _service.UpdateAsync(request.Id, request.Dto);
        return Unit.Value;
    }
}
