using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;
using MediatR;

namespace FarmManagement.Application.Features.Livestocks.Commands.DeleteLivestock;

public class DeleteLivestockHandler : ICommandHandler<DeleteLivestockCommand>
{
    private readonly ILivestockService _service;
    public DeleteLivestockHandler(ILivestockService service) => _service = service;

    public async Task<Unit> Handle(DeleteLivestockCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
