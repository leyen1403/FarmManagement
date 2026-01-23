// ***********************************************************************
// File: DeleteLocationStatusHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý command xóa trạng thái Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Locations;
using MediatR;

namespace FarmManagement.Application.Features.LocationStatuses.Commands.DeleteLocationStatus;

/// <summary>
/// Handler xử lý DeleteLocationStatusCommand.
/// </summary>
public class DeleteLocationStatusHandler : ICommandHandler<DeleteLocationStatusCommand>
{
    private readonly ILocationStatusService _locationStatusService;

    /// <summary>
    /// Khởi tạo DeleteLocationStatusHandler.
    /// </summary>
    /// <param name="locationStatusService">Service quản lý trạng thái Location.</param>
    public DeleteLocationStatusHandler(ILocationStatusService locationStatusService)
    {
        _locationStatusService = locationStatusService;
    }

    /// <summary>
    /// Xử lý command xóa trạng thái Location.
    /// </summary>
    /// <param name="request">Command chứa ID trạng thái Location cần xóa.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    public async Task<Unit> Handle(DeleteLocationStatusCommand request, CancellationToken cancellationToken)
    {
        await _locationStatusService.DeleteAsync(request.Id);

        return Unit.Value;
    }
}
