using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockTypes.Queries.GetLivestockTypeById;

public class GetLivestockTypeByIdHandler : IQueryHandler<GetLivestockTypeByIdQuery, LivestockTypeDto?>
{
    private readonly ILivestockTypeService _service;

    public GetLivestockTypeByIdHandler(ILivestockTypeService service) => _service = service;

    public async Task<LivestockTypeDto?> Handle(GetLivestockTypeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}
