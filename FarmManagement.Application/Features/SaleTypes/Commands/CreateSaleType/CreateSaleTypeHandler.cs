using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.SaleTypes.Commands.CreateSaleType;

public class CreateSaleTypeHandler : ICommandHandler<CreateSaleTypeCommand, int>
{
    private readonly ISaleTypeService _service;

    public CreateSaleTypeHandler(ISaleTypeService service) => _service = service;

    public async Task<int> Handle(CreateSaleTypeCommand request, CancellationToken cancellationToken)
    {
        return await _service.CreateAsync(request.Dto);
    }
}
