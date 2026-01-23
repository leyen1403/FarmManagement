using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;
using MediatR;

namespace FarmManagement.Application.Features.CostTypes.Commands.UpdateCostType;

public class UpdateCostTypeHandler : ICommandHandler<UpdateCostTypeCommand>
{
    private readonly ICostTypeService _service;

    public UpdateCostTypeHandler(ICostTypeService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(UpdateCostTypeCommand request, CancellationToken cancellationToken)
    {
        var dto = new UpdateCostTypeDto
        {
            Id = request.Id,
            Code = request.Code,
            Name = request.Name,
            Description = request.Description,
            IsActive = request.IsActive
        };

        await _service.UpdateAsync(request.Id, dto);
        return Unit.Value;
    }
}
