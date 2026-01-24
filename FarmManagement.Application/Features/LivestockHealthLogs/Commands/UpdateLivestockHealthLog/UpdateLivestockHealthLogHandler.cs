using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;
using MediatR;

namespace FarmManagement.Application.Features.LivestockHealthLogs.Commands.UpdateLivestockHealthLog;

public class UpdateLivestockHealthLogHandler : ICommandHandler<UpdateLivestockHealthLogCommand>
{
    private readonly ILivestockHealthLogService _service;

    public UpdateLivestockHealthLogHandler(ILivestockHealthLogService service) => _service = service;

    public async Task<Unit> Handle(UpdateLivestockHealthLogCommand request, CancellationToken cancellationToken)
    {
        await _service.UpdateAsync(request.Id, request.Dto);
        return Unit.Value;
    }
}
