using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockCareTypes.Commands.CreateLivestockCareType;

public class CreateLivestockCareTypeHandler : ICommandHandler<CreateLivestockCareTypeCommand, int>
{
    private readonly ILivestockCareTypeService _service;

    public CreateLivestockCareTypeHandler(ILivestockCareTypeService service) => _service = service;

    public async Task<int> Handle(CreateLivestockCareTypeCommand request, CancellationToken cancellationToken)
    {
        return await _service.CreateAsync(request.Dto);
    }
}
