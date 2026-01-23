// ***********************************************************************
// File: UpdateCropTypeHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý command cập nhật loại cây trồng.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;
using MediatR;

namespace FarmManagement.Application.Features.CropTypes.Commands.UpdateCropType;

/// <summary>
/// Handler xử lý UpdateCropTypeCommand.
/// </summary>
public class UpdateCropTypeHandler : ICommandHandler<UpdateCropTypeCommand>
{
    private readonly ICropTypeService _cropTypeService;

    /// <summary>
    /// Khởi tạo UpdateCropTypeHandler.
    /// </summary>
    /// <param name="cropTypeService">Service quản lý loại cây trồng.</param>
    public UpdateCropTypeHandler(ICropTypeService cropTypeService)
    {
        _cropTypeService = cropTypeService;
    }

    /// <summary>
    /// Xử lý command cập nhật loại cây trồng.
    /// </summary>
    /// <param name="request">Command chứa thông tin cập nhật.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    public async Task<Unit> Handle(UpdateCropTypeCommand request, CancellationToken cancellationToken)
    {
        var dto = new UpdateCropTypeDto
        {
            Id = request.Id,
            Code = request.Code,
            Name = request.Name,
            Description = request.Description,
            IsActive = request.IsActive
        };

        await _cropTypeService.UpdateAsync(request.Id, dto);

        return Unit.Value;
    }
}
