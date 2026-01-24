using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;
using MediatR;

namespace FarmManagement.Application.Features.LivestockCareTypes.Commands.DeleteLivestockCareType;

public class DeleteLivestockCareTypeHandler : ICommandHandler<DeleteLivestockCareTypeCommand>
{
    private readonly ILivestockCareTypeService _service;

    public DeleteLivestockCareTypeHandler(ILivestockCareTypeService service) => _service = service;

    public async Task<Unit> Handle(DeleteLivestockCareTypeCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
