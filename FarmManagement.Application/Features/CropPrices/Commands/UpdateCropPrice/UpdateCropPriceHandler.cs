using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;
using MediatR;

namespace FarmManagement.Application.Features.CropPrices.Commands.UpdateCropPrice;

public class UpdateCropPriceHandler : ICommandHandler<UpdateCropPriceCommand>
{
    private readonly ICropPriceService _cropPriceService;

    public UpdateCropPriceHandler(ICropPriceService cropPriceService)
    {
        _cropPriceService = cropPriceService;
    }

    public async Task<Unit> Handle(UpdateCropPriceCommand request, CancellationToken cancellationToken)
    {
        var dto = new UpdateCropPriceDto
        {
            Id = request.Id,
            CropId = request.CropId,
            Price = request.Price,
            Unit = request.Unit,
            EffectiveDate = request.EffectiveDate,
            ExpiryDate = request.ExpiryDate,
            Note = request.Note,
            IsActive = request.IsActive
        };

        await _cropPriceService.UpdateAsync(request.Id, dto);
        return Unit.Value;
    }
}
