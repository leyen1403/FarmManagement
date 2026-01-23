using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropHarvests.Commands.CreateCropHarvest;

public record CreateCropHarvestCommand(
    int CropId,
    DateTime HarvestDate,
    decimal Quantity,
    string? Unit,
    decimal UnitPrice,
    string? Buyer,
    string? Note
) : ICommand<int>;
