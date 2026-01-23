// ***********************************************************************
// File: CreateLocationStatusHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý command tạo mới trạng thái Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Application.Interfaces.Locations;

namespace FarmManagement.Application.Features.LocationStatuses.Commands.CreateLocationStatus;

/// <summary>
/// Handler xử lý CreateLocationStatusCommand.
/// </summary>
public class CreateLocationStatusHandler : ICommandHandler<CreateLocationStatusCommand, LocationStatusDto>
{
    private readonly ILocationStatusService _locationStatusService;

    /// <summary>
    /// Khởi tạo CreateLocationStatusHandler.
    /// </summary>
    /// <param name="locationStatusService">Service quản lý trạng thái Location.</param>
    public CreateLocationStatusHandler(ILocationStatusService locationStatusService)
    {
        _locationStatusService = locationStatusService;
    }

    /// <summary>
    /// Xử lý command tạo mới trạng thái Location.
    /// </summary>
    /// <param name="request">Command chứa thông tin trạng thái Location cần tạo.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    /// <returns>Trạng thái Location vừa tạo.</returns>
    public async Task<LocationStatusDto> Handle(CreateLocationStatusCommand request, CancellationToken cancellationToken)
    {
        var dto = new LocationStatusDto
        {
            Code = request.Code,
            Name = request.Name
        };

        return await _locationStatusService.CreateAsync(dto);
    }
}
