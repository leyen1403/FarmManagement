using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.LivestockCareTypes.Commands.DeleteLivestockCareType;

public record DeleteLivestockCareTypeCommand(int Id) : ICommand;
