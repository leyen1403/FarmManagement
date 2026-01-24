using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;
using MediatR;

namespace FarmManagement.Application.Features.LivestockTypes.Commands.UpdateLivestockType;

public class UpdateLivestockTypeHandler : ICommandHandler<UpdateLivestockTypeCommand>
{
    private readonly ILivestockTypeService _service;

    public UpdateLivestockTypeHandler(ILivestockTypeService service) => _service = service;

    public async Task<Unit> Handle(UpdateLivestockTypeCommand request, CancellationToken cancellationToken)
    {
        await _service.UpdateAsync(request.Id, request.Dto);
        return Unit.Value;
    }
}
