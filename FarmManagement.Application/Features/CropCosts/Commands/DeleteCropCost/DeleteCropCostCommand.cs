using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropCosts.Commands.DeleteCropCost;

public record DeleteCropCostCommand(int Id) : ICommand;
