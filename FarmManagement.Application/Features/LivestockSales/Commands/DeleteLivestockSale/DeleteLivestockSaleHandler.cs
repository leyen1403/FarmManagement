using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;
using MediatR;

namespace FarmManagement.Application.Features.LivestockSales.Commands.DeleteLivestockSale;

public class DeleteLivestockSaleHandler : ICommandHandler<DeleteLivestockSaleCommand>
{
    private readonly ILivestockSaleService _service;

    public DeleteLivestockSaleHandler(ILivestockSaleService service) => _service = service;

    public async Task<Unit> Handle(DeleteLivestockSaleCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
