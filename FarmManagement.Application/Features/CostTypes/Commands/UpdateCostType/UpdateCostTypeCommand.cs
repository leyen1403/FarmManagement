using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CostTypes.Commands.UpdateCostType;

public record UpdateCostTypeCommand(
    int Id,
    string Code,
    string Name,
    string? Description,
    bool IsActive
) : ICommand;
