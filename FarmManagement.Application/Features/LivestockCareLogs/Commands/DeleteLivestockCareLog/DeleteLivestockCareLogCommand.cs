using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.LivestockCareLogs.Commands.DeleteLivestockCareLog;

public record DeleteLivestockCareLogCommand(int Id) : ICommand;
