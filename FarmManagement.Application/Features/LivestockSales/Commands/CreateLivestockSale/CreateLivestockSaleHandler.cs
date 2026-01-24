using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockSales.Commands.CreateLivestockSale;

public class CreateLivestockSaleHandler : ICommandHandler<CreateLivestockSaleCommand, int>
{
    private readonly ILivestockSaleService _service;

    public CreateLivestockSaleHandler(ILivestockSaleService service) => _service = service;

    public async Task<int> Handle(CreateLivestockSaleCommand request, CancellationToken cancellationToken)
    {
        return await _service.CreateAsync(request.Dto);
    }
}
