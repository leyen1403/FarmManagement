using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;
using MediatR;

namespace FarmManagement.Application.Features.LivestockCareLogs.Commands.UpdateLivestockCareLog;

public class UpdateLivestockCareLogHandler : ICommandHandler<UpdateLivestockCareLogCommand>
{
    private readonly ILivestockCareLogService _service;

    public UpdateLivestockCareLogHandler(ILivestockCareLogService service) => _service = service;

    public async Task<Unit> Handle(UpdateLivestockCareLogCommand request, CancellationToken cancellationToken)
    {
        await _service.UpdateAsync(request.Id, request.Dto);
        return Unit.Value;
    }
}
