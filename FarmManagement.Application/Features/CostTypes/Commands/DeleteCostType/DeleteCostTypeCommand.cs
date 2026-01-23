using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CostTypes.Commands.DeleteCostType;

public record DeleteCostTypeCommand(int Id) : ICommand;
