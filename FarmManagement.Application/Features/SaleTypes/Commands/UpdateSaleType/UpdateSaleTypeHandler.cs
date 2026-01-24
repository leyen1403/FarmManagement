using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;
using MediatR;

namespace FarmManagement.Application.Features.SaleTypes.Commands.UpdateSaleType;

public class UpdateSaleTypeHandler : ICommandHandler<UpdateSaleTypeCommand>
{
    private readonly ISaleTypeService _service;

    public UpdateSaleTypeHandler(ISaleTypeService service) => _service = service;

    public async Task<Unit> Handle(UpdateSaleTypeCommand request, CancellationToken cancellationToken)
    {
        await _service.UpdateAsync(request.Id, request.Dto);
        return Unit.Value;
    }
}
