using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Crops;
using MediatR;

namespace FarmManagement.Application.Features.CropCosts.Commands.DeleteCropCost;

public class DeleteCropCostHandler : ICommandHandler<DeleteCropCostCommand>
{
    private readonly ICropCostService _service;

    public DeleteCropCostHandler(ICropCostService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(DeleteCropCostCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
