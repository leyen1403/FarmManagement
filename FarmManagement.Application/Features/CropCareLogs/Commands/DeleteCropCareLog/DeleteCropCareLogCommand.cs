using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropCareLogs.Commands.DeleteCropCareLog;

public record DeleteCropCareLogCommand(int Id) : ICommand;
