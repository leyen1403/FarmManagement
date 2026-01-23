using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Crops;
using MediatR;

namespace FarmManagement.Application.Features.CropPrices.Commands.DeleteCropPrice;

public class DeleteCropPriceHandler : ICommandHandler<DeleteCropPriceCommand>
{
    private readonly ICropPriceService _cropPriceService;

    public DeleteCropPriceHandler(ICropPriceService cropPriceService)
    {
        _cropPriceService = cropPriceService;
    }

    public async Task<Unit> Handle(DeleteCropPriceCommand request, CancellationToken cancellationToken)
    {
        await _cropPriceService.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
