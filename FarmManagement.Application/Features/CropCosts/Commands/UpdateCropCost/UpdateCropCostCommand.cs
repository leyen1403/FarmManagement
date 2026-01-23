using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropCosts.Commands.UpdateCropCost;

public record UpdateCropCostCommand(
    int Id,
    int CropId,
    int CostTypeId,
    DateTime CostDate,
    decimal Quantity,
    string? Unit,
    decimal UnitPrice,
    string? Note
) : ICommand;
