using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.Crops.Commands.CreateCrop;

public class CreateCropHandler : ICommandHandler<CreateCropCommand, int>
{
    private readonly ICropService _cropService;

    public CreateCropHandler(ICropService cropService)
    {
        _cropService = cropService;
    }

    public async Task<int> Handle(CreateCropCommand request, CancellationToken cancellationToken)
    {
        var dto = new CreateCropDto
        {
            Name = request.Name,
            CropTypeId = request.CropTypeId,
            LocationId = request.LocationId,
            CropStatusId = request.CropStatusId,
            PlantDate = request.PlantDate,
            ExpectedHarvestDate = request.ExpectedHarvestDate,
            EstimatedYield = request.EstimatedYield,
            Unit = request.Unit,
            Note = request.Note
        };

        return await _cropService.CreateAsync(dto);
    }
}
