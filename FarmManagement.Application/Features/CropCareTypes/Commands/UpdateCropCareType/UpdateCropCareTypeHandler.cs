using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;
using MediatR;

namespace FarmManagement.Application.Features.CropCareTypes.Commands.UpdateCropCareType;

public class UpdateCropCareTypeHandler : ICommandHandler<UpdateCropCareTypeCommand>
{
    private readonly ICropCareTypeService _service;

    public UpdateCropCareTypeHandler(ICropCareTypeService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(UpdateCropCareTypeCommand request, CancellationToken cancellationToken)
    {
        var dto = new UpdateCropCareTypeDto
        {
            Id = request.Id,
            Code = request.Code,
            Name = request.Name,
            Description = request.Description,
            IsActive = request.IsActive
        };

        await _service.UpdateAsync(request.Id, dto);
        return Unit.Value;
    }
}
