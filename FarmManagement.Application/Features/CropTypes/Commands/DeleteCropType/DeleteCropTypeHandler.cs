// ***********************************************************************
// File: DeleteCropTypeHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý command xóa loại cây trồng.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.Interfaces.Crops;
using MediatR;

namespace FarmManagement.Application.Features.CropTypes.Commands.DeleteCropType;

/// <summary>
/// Handler xử lý DeleteCropTypeCommand.
/// </summary>
public class DeleteCropTypeHandler : ICommandHandler<DeleteCropTypeCommand>
{
    private readonly ICropTypeService _cropTypeService;

    /// <summary>
    /// Khởi tạo DeleteCropTypeHandler.
    /// </summary>
    /// <param name="cropTypeService">Service quản lý loại cây trồng.</param>
    public DeleteCropTypeHandler(ICropTypeService cropTypeService)
    {
        _cropTypeService = cropTypeService;
    }

    /// <summary>
    /// Xử lý command xóa loại cây trồng.
    /// </summary>
    /// <param name="request">Command chứa ID loại cây trồng cần xóa.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    public async Task<Unit> Handle(DeleteCropTypeCommand request, CancellationToken cancellationToken)
    {
        await _cropTypeService.DeleteAsync(request.Id);

        return Unit.Value;
    }
}
