using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.LivestockHealthLogs.Commands.DeleteLivestockHealthLog;

public record DeleteLivestockHealthLogCommand(int Id) : ICommand;
