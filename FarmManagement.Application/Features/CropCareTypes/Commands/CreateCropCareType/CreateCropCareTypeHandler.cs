using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropCareTypes.Commands.CreateCropCareType;

public class CreateCropCareTypeHandler : ICommandHandler<CreateCropCareTypeCommand, int>
{
    private readonly ICropCareTypeService _service;

    public CreateCropCareTypeHandler(ICropCareTypeService service)
    {
        _service = service;
    }

    public async Task<int> Handle(CreateCropCareTypeCommand request, CancellationToken cancellationToken)
    {
        var dto = new CreateCropCareTypeDto
        {
            Code = request.Code,
            Name = request.Name,
            Description = request.Description
        };

        return await _service.CreateAsync(dto);
    }
}
