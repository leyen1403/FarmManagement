using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropCareTypes.Commands.DeleteCropCareType;

public record DeleteCropCareTypeCommand(int Id) : ICommand;
