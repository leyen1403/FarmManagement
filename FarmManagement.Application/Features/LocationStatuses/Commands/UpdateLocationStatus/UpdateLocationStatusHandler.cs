// ***********************************************************************
// File: UpdateLocationStatusHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý command cập nhật trạng thái Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Application.Interfaces.Locations;
using MediatR;

namespace FarmManagement.Application.Features.LocationStatuses.Commands.UpdateLocationStatus;

/// <summary>
/// Handler xử lý UpdateLocationStatusCommand.
/// </summary>
public class UpdateLocationStatusHandler : ICommandHandler<UpdateLocationStatusCommand>
{
    private readonly ILocationStatusService _locationStatusService;

    /// <summary>
    /// Khởi tạo UpdateLocationStatusHandler.
    /// </summary>
    /// <param name="locationStatusService">Service quản lý trạng thái Location.</param>
    public UpdateLocationStatusHandler(ILocationStatusService locationStatusService)
    {
        _locationStatusService = locationStatusService;
    }

    /// <summary>
    /// Xử lý command cập nhật trạng thái Location.
    /// </summary>
    /// <param name="request">Command chứa thông tin cập nhật.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    public async Task<Unit> Handle(UpdateLocationStatusCommand request, CancellationToken cancellationToken)
    {
        var dto = new LocationStatusDto
        {
            Id = request.Id,
            Code = request.Code,
            Name = request.Name
        };

        await _locationStatusService.UpdateAsync(dto);

        return Unit.Value;
    }
}
