using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropCareLogs.Commands.CreateCropCareLog;

public record CreateCropCareLogCommand(
    int CropId,
    int CropCareTypeId,
    DateTime CareDate,
    string? Description,
    decimal Cost
) : ICommand<int>;
