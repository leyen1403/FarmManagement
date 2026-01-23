using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Crops;
using MediatR;

namespace FarmManagement.Application.Features.CostTypes.Commands.DeleteCostType;

public class DeleteCostTypeHandler : ICommandHandler<DeleteCostTypeCommand>
{
    private readonly ICostTypeService _service;

    public DeleteCostTypeHandler(ICostTypeService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(DeleteCostTypeCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
