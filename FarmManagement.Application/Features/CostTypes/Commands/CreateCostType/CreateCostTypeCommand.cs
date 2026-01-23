using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CostTypes.Commands.CreateCostType;

public record CreateCostTypeCommand(
    string Code,
    string Name,
    string? Description
) : ICommand<int>;