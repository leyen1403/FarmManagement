using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;
using MediatR;

namespace FarmManagement.Application.Features.LivestockSales.Commands.UpdateLivestockSale;

public class UpdateLivestockSaleHandler : ICommandHandler<UpdateLivestockSaleCommand>
{
    private readonly ILivestockSaleService _service;

    public UpdateLivestockSaleHandler(ILivestockSaleService service) => _service = service;

    public async Task<Unit> Handle(UpdateLivestockSaleCommand request, CancellationToken cancellationToken)
    {
        await _service.UpdateAsync(request.Id, request.Dto);
        return Unit.Value;
    }
}
