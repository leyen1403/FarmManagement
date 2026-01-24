using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;
using MediatR;

namespace FarmManagement.Application.Features.Livestocks.Commands.UpdateLivestock;

public class UpdateLivestockHandler : ICommandHandler<UpdateLivestockCommand>
{
    private readonly ILivestockService _service;
    public UpdateLivestockHandler(ILivestockService service) => _service = service;

    public async Task<Unit> Handle(UpdateLivestockCommand request, CancellationToken cancellationToken)
    {
        await _service.UpdateAsync(request.Id, request.Dto);
        return Unit.Value;
    }
}
