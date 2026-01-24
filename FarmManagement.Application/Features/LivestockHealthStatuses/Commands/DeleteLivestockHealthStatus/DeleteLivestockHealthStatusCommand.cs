using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.LivestockHealthStatuses.Commands.DeleteLivestockHealthStatus;

public record DeleteLivestockHealthStatusCommand(int Id) : ICommand;
