// ***********************************************************************
// File: GetCropTypeByIdHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý query lấy loại cây trồng theo ID.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropTypes.Queries.GetCropTypeById;

/// <summary>
/// Handler xử lý GetCropTypeByIdQuery.
/// </summary>
public class GetCropTypeByIdHandler : IQueryHandler<GetCropTypeByIdQuery, CropTypeDto?>
{
    private readonly ICropTypeService _cropTypeService;

    /// <summary>
    /// Khởi tạo GetCropTypeByIdHandler.
    /// </summary>
    /// <param name="cropTypeService">Service quản lý loại cây trồng.</param>
    public GetCropTypeByIdHandler(ICropTypeService cropTypeService)
    {
        _cropTypeService = cropTypeService;
    }

    /// <summary>
    /// Xử lý query lấy loại cây trồng theo ID.
    /// </summary>
    /// <param name="request">Query chứa ID loại cây trồng.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    /// <returns>Loại cây trồng tìm được hoặc null.</returns>
    public async Task<CropTypeDto?> Handle(GetCropTypeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _cropTypeService.GetByIdAsync(request.Id);
    }
}
