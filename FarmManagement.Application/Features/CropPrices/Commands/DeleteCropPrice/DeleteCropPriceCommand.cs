using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropPrices.Commands.DeleteCropPrice;

public record DeleteCropPriceCommand(int Id) : ICommand;
