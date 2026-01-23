// ***********************************************************************
// File: DeleteLocationHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý command xóa Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Locations;
using MediatR;

namespace FarmManagement.Application.Features.Locations.Commands.DeleteLocation;

/// <summary>
/// Handler xử lý DeleteLocationCommand.
/// </summary>
public class DeleteLocationHandler : ICommandHandler<DeleteLocationCommand>
{
    private readonly ILocationService _locationService;

    /// <summary>
    /// Khởi tạo DeleteLocationHandler.
    /// </summary>
    /// <param name="locationService">Service quản lý Location.</param>
    public DeleteLocationHandler(ILocationService locationService)
    {
        _locationService = locationService;
    }

    /// <summary>
    /// Xử lý command xóa Location.
    /// </summary>
    /// <param name="request">Command chứa ID Location cần xóa.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    public async Task<Unit> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        await _locationService.DeleteAsync(request.Id);

        return Unit.Value;
    }
}
