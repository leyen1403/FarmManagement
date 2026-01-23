using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropPrices.Commands.UpdateCropPrice;

public record UpdateCropPriceCommand(
    int Id,
    int CropId,
    decimal Price,
    string Unit,
    DateTime EffectiveDate,
    DateTime? ExpiryDate,
    string? Note,
    bool IsActive
) : ICommand;
