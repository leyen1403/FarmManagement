using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.Livestocks.Commands.CreateLivestock;

public class CreateLivestockHandler : ICommandHandler<CreateLivestockCommand, int>
{
    private readonly ILivestockService _service;
    public CreateLivestockHandler(ILivestockService service) => _service = service;

    public async Task<int> Handle(CreateLivestockCommand request, CancellationToken cancellationToken)
    {
        return await _service.CreateAsync(request.Dto);
    }
}
