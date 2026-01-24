using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockHealthLogs.Commands.CreateLivestockHealthLog;

public class CreateLivestockHealthLogHandler : ICommandHandler<CreateLivestockHealthLogCommand, int>
{
    private readonly ILivestockHealthLogService _service;

    public CreateLivestockHealthLogHandler(ILivestockHealthLogService service) => _service = service;

    public async Task<int> Handle(CreateLivestockHealthLogCommand request, CancellationToken cancellationToken)
    {
        return await _service.CreateAsync(request.Dto);
    }
}
