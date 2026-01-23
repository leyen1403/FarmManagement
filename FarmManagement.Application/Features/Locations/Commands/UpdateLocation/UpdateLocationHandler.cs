// ***********************************************************************
// File: UpdateLocationHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý command cập nhật Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Application.Interfaces.Locations;
using MediatR;

namespace FarmManagement.Application.Features.Locations.Commands.UpdateLocation;

/// <summary>
/// Handler xử lý UpdateLocationCommand.
/// </summary>
public class UpdateLocationHandler : ICommandHandler<UpdateLocationCommand>
{
    private readonly ILocationService _locationService;

    /// <summary>
    /// Khởi tạo UpdateLocationHandler.
    /// </summary>
    /// <param name="locationService">Service quản lý Location.</param>
    public UpdateLocationHandler(ILocationService locationService)
    {
        _locationService = locationService;
    }

    /// <summary>
    /// Xử lý command cập nhật Location.
    /// </summary>
    /// <param name="request">Command chứa thông tin cập nhật.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    public async Task<Unit> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        var dto = new LocationDto
        {
            Id = request.Id,
            Name = request.Name,
            LocationTypeId = request.LocationTypeId,
            LocationStatusId = request.LocationStatusId,
            Address = request.Address,
            Description = request.Description,
            Area = request.Area,
            Capacity = request.Capacity,
            CapacityUnit = request.CapacityUnit,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            ParentLocationId = request.ParentLocationId,
            Note = request.Note
        };

        await _locationService.UpdateAsync(dto);

        return Unit.Value;
    }
}
