using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropPrices.Commands.CreateCropPrice;

public record CreateCropPriceCommand(
    int CropId,
    decimal Price,
    string Unit,
    DateTime EffectiveDate,
    DateTime? ExpiryDate,
    string? Note
) : ICommand<int>;
