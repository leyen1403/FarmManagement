using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.Livestocks.Commands.DeleteLivestock;

public record DeleteLivestockCommand(int Id) : ICommand;
