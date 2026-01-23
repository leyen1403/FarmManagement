using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.Crops.Commands.UpdateCrop;

/// <summary>
/// Command cập nhật cây trồng.
/// </summary>
public record UpdateCropCommand(
    int Id,
    string Name,
    int CropTypeId,
    int LocationId,
    int CropStatusId,
    DateTime PlantDate,
    DateTime? ExpectedHarvestDate,
    DateTime? ActualHarvestDate,
    decimal? EstimatedYield,
    string? Unit,
    string? Note
) : ICommand;
