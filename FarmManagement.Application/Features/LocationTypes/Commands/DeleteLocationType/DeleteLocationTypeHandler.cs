// ***********************************************************************
// File: DeleteLocationTypeHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý command xóa loại Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Locations;
using MediatR;

namespace FarmManagement.Application.Features.LocationTypes.Commands.DeleteLocationType;

/// <summary>
/// Handler xử lý DeleteLocationTypeCommand.
/// </summary>
public class DeleteLocationTypeHandler : ICommandHandler<DeleteLocationTypeCommand>
{
    private readonly ILocationTypeService _locationTypeService;

    /// <summary>
    /// Khởi tạo DeleteLocationTypeHandler.
    /// </summary>
    /// <param name="locationTypeService">Service quản lý loại Location.</param>
    public DeleteLocationTypeHandler(ILocationTypeService locationTypeService)
    {
        _locationTypeService = locationTypeService;
    }

    /// <summary>
    /// Xử lý command xóa loại Location.
    /// </summary>
    /// <param name="request">Command chứa ID loại Location cần xóa.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    public async Task<Unit> Handle(DeleteLocationTypeCommand request, CancellationToken cancellationToken)
    {
        await _locationTypeService.DeleteAsync(request.Id);

        return Unit.Value;
    }
}
