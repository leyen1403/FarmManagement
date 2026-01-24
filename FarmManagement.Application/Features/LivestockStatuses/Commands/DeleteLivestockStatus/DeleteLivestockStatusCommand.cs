using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.LivestockStatuses.Commands.DeleteLivestockStatus;

public record DeleteLivestockStatusCommand(int Id) : ICommand;
