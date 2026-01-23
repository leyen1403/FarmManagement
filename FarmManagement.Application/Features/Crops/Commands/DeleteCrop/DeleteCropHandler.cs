using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Crops;
using MediatR;

namespace FarmManagement.Application.Features.Crops.Commands.DeleteCrop;

public class DeleteCropHandler : ICommandHandler<DeleteCropCommand>
{
    private readonly ICropService _cropService;

    public DeleteCropHandler(ICropService cropService)
    {
        _cropService = cropService;
    }

    public async Task<Unit> Handle(DeleteCropCommand request, CancellationToken cancellationToken)
    {
        await _cropService.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
