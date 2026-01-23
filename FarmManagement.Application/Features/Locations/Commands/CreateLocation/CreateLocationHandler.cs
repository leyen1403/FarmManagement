// ***********************************************************************
// File: CreateLocationHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý command tạo mới Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Application.Interfaces.Locations;

namespace FarmManagement.Application.Features.Locations.Commands.CreateLocation;

/// <summary>
/// Handler xử lý CreateLocationCommand.
/// </summary>
public class CreateLocationHandler : ICommandHandler<CreateLocationCommand, LocationDto>
{
    private readonly ILocationService _locationService;

    /// <summary>
    /// Khởi tạo CreateLocationHandler.
    /// </summary>
    /// <param name="locationService">Service quản lý Location.</param>
    public CreateLocationHandler(ILocationService locationService)
    {
        _locationService = locationService;
    }

    /// <summary>
    /// Xử lý command tạo mới Location.
    /// </summary>
    /// <param name="request">Command chứa thông tin Location cần tạo.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    /// <returns>Location vừa tạo.</returns>
    public async Task<LocationDto> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        var dto = new LocationDto
        {
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

        var result = await _locationService.CreateAsync(dto);

        return result;
    }
}
