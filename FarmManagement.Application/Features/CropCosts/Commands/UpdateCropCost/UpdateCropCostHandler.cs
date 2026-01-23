using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;
using MediatR;

namespace FarmManagement.Application.Features.CropCosts.Commands.UpdateCropCost;

public class UpdateCropCostHandler : ICommandHandler<UpdateCropCostCommand>
{
    private readonly ICropCostService _service;

    public UpdateCropCostHandler(ICropCostService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(UpdateCropCostCommand request, CancellationToken cancellationToken)
    {
        var dto = new UpdateCropCostDto
        {
            Id = request.Id,
            CropId = request.CropId,
            CostTypeId = request.CostTypeId,
            CostDate = request.CostDate,
            Quantity = request.Quantity,
            Unit = request.Unit,
            UnitPrice = request.UnitPrice,
            Note = request.Note
        };

        await _service.UpdateAsync(request.Id, dto);
        return Unit.Value;
    }
}
