// ***********************************************************************
// File: DeleteCropStatusHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý command xóa trạng thái cây trồng.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Crops;
using MediatR;

namespace FarmManagement.Application.Features.CropStatuses.Commands.DeleteCropStatus;

/// <summary>
/// Handler xử lý DeleteCropStatusCommand.
/// </summary>
public class DeleteCropStatusHandler : ICommandHandler<DeleteCropStatusCommand>
{
    private readonly ICropStatusService _cropStatusService;

    /// <summary>
    /// Khởi tạo DeleteCropStatusHandler.
    /// </summary>
    /// <param name="cropStatusService">Service quản lý trạng thái cây trồng.</param>
    public DeleteCropStatusHandler(ICropStatusService cropStatusService)
    {
        _cropStatusService = cropStatusService;
    }

    /// <summary>
    /// Xử lý command xóa trạng thái cây trồng.
    /// </summary>
    /// <param name="request">Command chứa ID trạng thái cây trồng cần xóa.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    public async Task<Unit> Handle(DeleteCropStatusCommand request, CancellationToken cancellationToken)
    {
        await _cropStatusService.DeleteAsync(request.Id);

        return Unit.Value;
    }
}
