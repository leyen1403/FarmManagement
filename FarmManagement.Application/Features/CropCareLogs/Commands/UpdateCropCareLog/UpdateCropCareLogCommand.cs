using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropCareLogs.Commands.UpdateCropCareLog;

public record UpdateCropCareLogCommand(
    int Id,
    int CropId,
    int CropCareTypeId,
    DateTime CareDate,
    string? Description,
    decimal Cost
) : ICommand;
