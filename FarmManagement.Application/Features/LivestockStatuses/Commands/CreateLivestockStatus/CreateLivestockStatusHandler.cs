using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockStatuses.Commands.CreateLivestockStatus;

public class CreateLivestockStatusHandler : ICommandHandler<CreateLivestockStatusCommand, int>
{
    private readonly ILivestockStatusService _service;

    public CreateLivestockStatusHandler(ILivestockStatusService service) => _service = service;

    public async Task<int> Handle(CreateLivestockStatusCommand request, CancellationToken cancellationToken)
    {
        return await _service.CreateAsync(request.Dto);
    }
}
