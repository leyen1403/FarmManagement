using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropCareTypes.Commands.CreateCropCareType;

public record CreateCropCareTypeCommand(
    string Code,
    string Name,
    string? Description
) : ICommand<int>;
