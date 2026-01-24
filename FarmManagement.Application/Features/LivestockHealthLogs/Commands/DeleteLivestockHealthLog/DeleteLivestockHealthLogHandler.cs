using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;
using MediatR;

namespace FarmManagement.Application.Features.LivestockHealthLogs.Commands.DeleteLivestockHealthLog;

public class DeleteLivestockHealthLogHandler : ICommandHandler<DeleteLivestockHealthLogCommand>
{
    private readonly ILivestockHealthLogService _service;

    public DeleteLivestockHealthLogHandler(ILivestockHealthLogService service) => _service = service;

    public async Task<Unit> Handle(DeleteLivestockHealthLogCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
