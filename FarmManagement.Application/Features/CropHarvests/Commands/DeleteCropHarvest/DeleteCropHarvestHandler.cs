using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Crops;
using MediatR;

namespace FarmManagement.Application.Features.CropHarvests.Commands.DeleteCropHarvest;

public class DeleteCropHarvestHandler : ICommandHandler<DeleteCropHarvestCommand>
{
    private readonly ICropHarvestService _service;

    public DeleteCropHarvestHandler(ICropHarvestService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(DeleteCropHarvestCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
