// ***********************************************************************
// File: GetAllCropTypesHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý query lấy tất cả loại cây trồng.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropTypes.Queries.GetAllCropTypes;

/// <summary>
/// Handler xử lý GetAllCropTypesQuery.
/// </summary>
public class GetAllCropTypesHandler : IQueryHandler<GetAllCropTypesQuery, List<CropTypeDto>>
{
    private readonly ICropTypeService _cropTypeService;

    /// <summary>
    /// Khởi tạo GetAllCropTypesHandler.
    /// </summary>
    /// <param name="cropTypeService">Service quản lý loại cây trồng.</param>
    public GetAllCropTypesHandler(ICropTypeService cropTypeService)
    {
        _cropTypeService = cropTypeService;
    }

    /// <summary>
    /// Xử lý query lấy tất cả loại cây trồng.
    /// </summary>
    /// <param name="request">Query chứa điều kiện lọc.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    /// <returns>Danh sách loại cây trồng.</returns>
    public async Task<List<CropTypeDto>> Handle(GetAllCropTypesQuery request, CancellationToken cancellationToken)
    {
        return await _cropTypeService.GetAllAsync(request.IncludeInactive);
    }
}
