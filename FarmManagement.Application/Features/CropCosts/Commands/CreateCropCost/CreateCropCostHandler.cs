using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropCosts.Commands.CreateCropCost;

public class CreateCropCostHandler : ICommandHandler<CreateCropCostCommand, int>
{
    private readonly ICropCostService _service;

    public CreateCropCostHandler(ICropCostService service)
    {
        _service = service;
    }

    public async Task<int> Handle(CreateCropCostCommand request, CancellationToken cancellationToken)
    {
        var dto = new CreateCropCostDto
        {
            CropId = request.CropId,
            CostTypeId = request.CostTypeId,
            CostDate = request.CostDate,
            Quantity = request.Quantity,
            Unit = request.Unit,
            UnitPrice = request.UnitPrice,
            Note = request.Note
        };

        return await _service.CreateAsync(dto);
    }
}
