// ***********************************************************************
// File: CreateCropTypeHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý command tạo mới loại cây trồng.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropTypes.Commands.CreateCropType;

/// <summary>
/// Handler xử lý CreateCropTypeCommand.
/// </summary>
public class CreateCropTypeHandler : ICommandHandler<CreateCropTypeCommand, int>
{
    private readonly ICropTypeService _cropTypeService;

    /// <summary>
    /// Khởi tạo CreateCropTypeHandler.
    /// </summary>
    /// <param name="cropTypeService">Service quản lý loại cây trồng.</param>
    public CreateCropTypeHandler(ICropTypeService cropTypeService)
    {
        _cropTypeService = cropTypeService;
    }

    /// <summary>
    /// Xử lý command tạo mới loại cây trồng.
    /// </summary>
    /// <param name="request">Command chứa thông tin loại cây trồng cần tạo.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    /// <returns>ID của loại cây trồng vừa tạo.</returns>
    public async Task<int> Handle(CreateCropTypeCommand request, CancellationToken cancellationToken)
    {
        var dto = new CreateCropTypeDto
        {
            Code = request.Code,
            Name = request.Name,
            Description = request.Description
        };

        return await _cropTypeService.CreateAsync(dto);
    }
}
