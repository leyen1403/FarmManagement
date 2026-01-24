using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;
using MediatR;

namespace FarmManagement.Application.Features.LivestockTypes.Commands.DeleteLivestockType;

public class DeleteLivestockTypeHandler : ICommandHandler<DeleteLivestockTypeCommand>
{
    private readonly ILivestockTypeService _service;

    public DeleteLivestockTypeHandler(ILivestockTypeService service) => _service = service;

    public async Task<Unit> Handle(DeleteLivestockTypeCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
