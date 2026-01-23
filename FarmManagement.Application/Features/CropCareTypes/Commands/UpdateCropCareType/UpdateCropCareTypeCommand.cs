using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropCareTypes.Commands.UpdateCropCareType;

public record UpdateCropCareTypeCommand(
    int Id,
    string Code,
    string Name,
    string? Description,
    bool IsActive
) : ICommand;
