using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;
using MediatR;

namespace FarmManagement.Application.Features.LivestockCareTypes.Commands.UpdateLivestockCareType;

public class UpdateLivestockCareTypeHandler : ICommandHandler<UpdateLivestockCareTypeCommand>
{
    private readonly ILivestockCareTypeService _service;

    public UpdateLivestockCareTypeHandler(ILivestockCareTypeService service) => _service = service;

    public async Task<Unit> Handle(UpdateLivestockCareTypeCommand request, CancellationToken cancellationToken)
    {
        await _service.UpdateAsync(request.Id, request.Dto);
        return Unit.Value;
    }
}
