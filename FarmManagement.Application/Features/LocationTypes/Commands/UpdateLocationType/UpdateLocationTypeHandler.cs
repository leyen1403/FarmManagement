// ***********************************************************************
// File: UpdateLocationTypeHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý command cập nhật loại Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Application.Interfaces.Locations;
using MediatR;

namespace FarmManagement.Application.Features.LocationTypes.Commands.UpdateLocationType;

/// <summary>
/// Handler xử lý UpdateLocationTypeCommand.
/// </summary>
public class UpdateLocationTypeHandler : ICommandHandler<UpdateLocationTypeCommand>
{
    private readonly ILocationTypeService _locationTypeService;

    /// <summary>
    /// Khởi tạo UpdateLocationTypeHandler.
    /// </summary>
    /// <param name="locationTypeService">Service quản lý loại Location.</param>
    public UpdateLocationTypeHandler(ILocationTypeService locationTypeService)
    {
        _locationTypeService = locationTypeService;
    }

    /// <summary>
    /// Xử lý command cập nhật loại Location.
    /// </summary>
    /// <param name="request">Command chứa thông tin cập nhật.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    public async Task<Unit> Handle(UpdateLocationTypeCommand request, CancellationToken cancellationToken)
    {
        var dto = new LocationTypeDto
        {
            Id = request.Id,
            Code = request.Code,
            Name = request.Name
        };

        await _locationTypeService.UpdateAsync(dto);

        return Unit.Value;
    }
}
