using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropHarvests.Commands.DeleteCropHarvest;

public record DeleteCropHarvestCommand(int Id) : ICommand;
