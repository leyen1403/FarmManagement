using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Crops;
using MediatR;

namespace FarmManagement.Application.Features.CropCareTypes.Commands.DeleteCropCareType;

public class DeleteCropCareTypeHandler : ICommandHandler<DeleteCropCareTypeCommand>
{
    private readonly ICropCareTypeService _service;

    public DeleteCropCareTypeHandler(ICropCareTypeService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(DeleteCropCareTypeCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
