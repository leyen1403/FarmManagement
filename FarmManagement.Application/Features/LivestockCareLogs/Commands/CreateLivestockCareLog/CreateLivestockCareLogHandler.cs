using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockCareLogs.Commands.CreateLivestockCareLog;

public class CreateLivestockCareLogHandler : ICommandHandler<CreateLivestockCareLogCommand, int>
{
    private readonly ILivestockCareLogService _service;

    public CreateLivestockCareLogHandler(ILivestockCareLogService service) => _service = service;

    public async Task<int> Handle(CreateLivestockCareLogCommand request, CancellationToken cancellationToken)
    {
        return await _service.CreateAsync(request.Dto);
    }
}
