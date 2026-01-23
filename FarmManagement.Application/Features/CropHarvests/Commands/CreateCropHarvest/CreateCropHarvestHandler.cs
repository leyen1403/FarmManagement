using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropHarvests.Commands.CreateCropHarvest;

public class CreateCropHarvestHandler : ICommandHandler<CreateCropHarvestCommand, int>
{
    private readonly ICropHarvestService _service;

    public CreateCropHarvestHandler(ICropHarvestService service)
    {
        _service = service;
    }

    public async Task<int> Handle(CreateCropHarvestCommand request, CancellationToken cancellationToken)
    {
        var dto = new CreateCropHarvestDto
        {
            CropId = request.CropId,
            HarvestDate = request.HarvestDate,
            Quantity = request.Quantity,
            Unit = request.Unit,
            UnitPrice = request.UnitPrice,
            Buyer = request.Buyer,
            Note = request.Note
        };

        return await _service.CreateAsync(dto);
    }
}
