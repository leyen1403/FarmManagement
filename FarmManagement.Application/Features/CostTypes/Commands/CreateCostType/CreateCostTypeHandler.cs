using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CostTypes.Commands.CreateCostType;

public class CreateCostTypeHandler : ICommandHandler<CreateCostTypeCommand, int>
{
    private readonly ICostTypeService _service;

    public CreateCostTypeHandler(ICostTypeService service)
    {
        _service = service;
    }

    public async Task<int> Handle(CreateCostTypeCommand request, CancellationToken cancellationToken)
    {
        var dto = new CreateCostTypeDto
        {
            Code = request.Code,
            Name = request.Name,
            Description = request.Description
        };

        return await _service.CreateAsync(dto);
    }
}
