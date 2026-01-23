using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropCosts.Commands.CreateCropCost;

public record CreateCropCostCommand(
    int CropId,
    int CostTypeId,
    DateTime CostDate,
    decimal Quantity,
    string? Unit,
    decimal UnitPrice,
    string? Note
) : ICommand<int>;
