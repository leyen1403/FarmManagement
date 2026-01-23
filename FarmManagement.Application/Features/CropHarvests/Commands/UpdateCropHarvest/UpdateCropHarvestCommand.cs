using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropHarvests.Commands.UpdateCropHarvest;

public record UpdateCropHarvestCommand(
    int Id,
    int CropId,
    DateTime HarvestDate,
    decimal Quantity,
    string? Unit,
    decimal UnitPrice,
    string? Buyer,
    string? Note
) : ICommand;
