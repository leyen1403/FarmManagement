using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.LivestockTypes.Commands.DeleteLivestockType;

public record DeleteLivestockTypeCommand(int Id) : ICommand;
