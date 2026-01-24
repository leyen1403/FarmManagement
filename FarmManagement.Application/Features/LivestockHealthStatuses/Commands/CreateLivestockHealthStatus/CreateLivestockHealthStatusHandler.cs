using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockHealthStatuses.Commands.CreateLivestockHealthStatus;

public class CreateLivestockHealthStatusHandler : ICommandHandler<CreateLivestockHealthStatusCommand, int>
{
    private readonly ILivestockHealthStatusService _service;

    public CreateLivestockHealthStatusHandler(ILivestockHealthStatusService service) => _service = service;

    public async Task<int> Handle(CreateLivestockHealthStatusCommand request, CancellationToken cancellationToken)
    {
        return await _service.CreateAsync(request.Dto);
    }
}
