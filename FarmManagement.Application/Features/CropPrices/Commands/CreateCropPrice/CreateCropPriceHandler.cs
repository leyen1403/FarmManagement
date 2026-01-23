using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropPrices.Commands.CreateCropPrice;

public class CreateCropPriceHandler : ICommandHandler<CreateCropPriceCommand, int>
{
    private readonly ICropPriceService _cropPriceService;

    public CreateCropPriceHandler(ICropPriceService cropPriceService)
    {
        _cropPriceService = cropPriceService;
    }

    public async Task<int> Handle(CreateCropPriceCommand request, CancellationToken cancellationToken)
    {
        var dto = new CreateCropPriceDto
        {
            CropId = request.CropId,
            Price = request.Price,
            Unit = request.Unit,
            EffectiveDate = request.EffectiveDate,
            ExpiryDate = request.ExpiryDate,
            Note = request.Note
        };

        return await _cropPriceService.CreateAsync(dto);
    }
}
