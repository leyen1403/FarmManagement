using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.Crops.Commands.CreateCrop;

/// <summary>
/// Command tạo mới cây trồng.
/// </summary>
public record CreateCropCommand(
    string Name,
    int CropTypeId,
    int LocationId,
    int CropStatusId,
    DateTime PlantDate,
    DateTime? ExpectedHarvestDate,
    decimal? EstimatedYield,
    string? Unit,
    string? Note
) : ICommand<int>;
