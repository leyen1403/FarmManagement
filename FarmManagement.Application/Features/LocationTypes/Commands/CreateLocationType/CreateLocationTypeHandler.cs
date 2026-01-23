// ***********************************************************************
// File: CreateLocationTypeHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý command tạo mới loại Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Application.Interfaces.Locations;

namespace FarmManagement.Application.Features.LocationTypes.Commands.CreateLocationType;

/// <summary>
/// Handler xử lý CreateLocationTypeCommand.
/// </summary>
public class CreateLocationTypeHandler : ICommandHandler<CreateLocationTypeCommand, LocationTypeDto>
{
    private readonly ILocationTypeService _locationTypeService;

    /// <summary>
    /// Khởi tạo CreateLocationTypeHandler.
    /// </summary>
    /// <param name="locationTypeService">Service quản lý loại Location.</param>
    public CreateLocationTypeHandler(ILocationTypeService locationTypeService)
    {
        _locationTypeService = locationTypeService;
    }

    /// <summary>
    /// Xử lý command tạo mới loại Location.
    /// </summary>
    /// <param name="request">Command chứa thông tin loại Location cần tạo.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    /// <returns>Loại Location vừa tạo.</returns>
    public async Task<LocationTypeDto> Handle(CreateLocationTypeCommand request, CancellationToken cancellationToken)
    {
        var dto = new LocationTypeDto
        {
            Code = request.Code,
            Name = request.Name
        };

        return await _locationTypeService.CreateAsync(dto);
    }
}
