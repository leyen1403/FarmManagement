using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;
using MediatR;

namespace FarmManagement.Application.Features.SaleTypes.Commands.DeleteSaleType;

public class DeleteSaleTypeHandler : ICommandHandler<DeleteSaleTypeCommand>
{
    private readonly ISaleTypeService _service;

    public DeleteSaleTypeHandler(ISaleTypeService service) => _service = service;

    public async Task<Unit> Handle(DeleteSaleTypeCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
