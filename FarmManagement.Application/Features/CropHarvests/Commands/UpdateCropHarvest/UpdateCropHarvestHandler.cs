using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;
using MediatR;

namespace FarmManagement.Application.Features.CropHarvests.Commands.UpdateCropHarvest;

public class UpdateCropHarvestHandler : ICommandHandler<UpdateCropHarvestCommand>
{
    private readonly ICropHarvestService _service;

    public UpdateCropHarvestHandler(ICropHarvestService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(UpdateCropHarvestCommand request, CancellationToken cancellationToken)
    {
        var dto = new UpdateCropHarvestDto
        {
            Id = request.Id,
            CropId = request.CropId,
            HarvestDate = request.HarvestDate,
            Quantity = request.Quantity,
            Unit = request.Unit,
            UnitPrice = request.UnitPrice,
            Buyer = request.Buyer,
            Note = request.Note
        };

        await _service.UpdateAsync(request.Id, dto);
        return Unit.Value;
    }
}
