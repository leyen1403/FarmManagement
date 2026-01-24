using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Livestocks;

namespace FarmManagement.Application.Features.LivestockTypes.Commands.CreateLivestockType;

public class CreateLivestockTypeHandler : ICommandHandler<CreateLivestockTypeCommand, int>
{
    private readonly ILivestockTypeService _service;

    public CreateLivestockTypeHandler(ILivestockTypeService service) => _service = service;

    public async Task<int> Handle(CreateLivestockTypeCommand request, CancellationToken cancellationToken)
    {
        return await _service.CreateAsync(request.Dto);
    }
}
