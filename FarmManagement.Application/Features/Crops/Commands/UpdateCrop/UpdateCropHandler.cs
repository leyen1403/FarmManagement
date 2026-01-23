using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;
using MediatR;

namespace FarmManagement.Application.Features.Crops.Commands.UpdateCrop;

public class UpdateCropHandler : ICommandHandler<UpdateCropCommand>
{
    private readonly ICropService _cropService;

    public UpdateCropHandler(ICropService cropService)
    {
        _cropService = cropService;
    }

    public async Task<Unit> Handle(UpdateCropCommand request, CancellationToken cancellationToken)
    {
        var dto = new UpdateCropDto
        {
            Id = request.Id,
            Name = request.Name,
            CropTypeId = request.CropTypeId,
            LocationId = request.LocationId,
            CropStatusId = request.CropStatusId,
            PlantDate = request.PlantDate,
            ExpectedHarvestDate = request.ExpectedHarvestDate,
            ActualHarvestDate = request.ActualHarvestDate,
            EstimatedYield = request.EstimatedYield,
            Unit = request.Unit,
            Note = request.Note
        };

        await _cropService.UpdateAsync(request.Id, dto);
        return Unit.Value;
    }
}
