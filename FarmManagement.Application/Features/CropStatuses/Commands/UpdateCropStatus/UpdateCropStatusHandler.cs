// ***********************************************************************
// File: UpdateCropStatusHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý command cập nhật trạng thái cây trồng.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;
using MediatR;

namespace FarmManagement.Application.Features.CropStatuses.Commands.UpdateCropStatus;

/// <summary>
/// Handler xử lý UpdateCropStatusCommand.
/// </summary>
public class UpdateCropStatusHandler : ICommandHandler<UpdateCropStatusCommand>
{
    private readonly ICropStatusService _cropStatusService;

    /// <summary>
    /// Khởi tạo UpdateCropStatusHandler.
    /// </summary>
    /// <param name="cropStatusService">Service quản lý trạng thái cây trồng.</param>
    public UpdateCropStatusHandler(ICropStatusService cropStatusService)
    {
        _cropStatusService = cropStatusService;
    }

    /// <summary>
    /// Xử lý command cập nhật trạng thái cây trồng.
    /// </summary>
    /// <param name="request">Command chứa thông tin cập nhật.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    public async Task<Unit> Handle(UpdateCropStatusCommand request, CancellationToken cancellationToken)
    {
        var dto = new UpdateCropStatusDto
        {
            Id = request.Id,
            Code = request.Code,
            Name = request.Name,
            Description = request.Description,
            IsActive = request.IsActive
        };

        await _cropStatusService.UpdateAsync(request.Id, dto);

        return Unit.Value;
    }
}
