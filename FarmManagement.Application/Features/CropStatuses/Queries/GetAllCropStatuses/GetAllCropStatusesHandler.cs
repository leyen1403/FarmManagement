// ***********************************************************************
// File: GetAllCropStatusesHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý query lấy tất cả trạng thái cây trồng.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropStatuses.Queries.GetAllCropStatuses;

/// <summary>
/// Handler xử lý GetAllCropStatusesQuery.
/// </summary>
public class GetAllCropStatusesHandler : IQueryHandler<GetAllCropStatusesQuery, IEnumerable<CropStatusDto>>
{
    private readonly ICropStatusService _cropStatusService;

    /// <summary>
    /// Khởi tạo GetAllCropStatusesHandler.
    /// </summary>
    /// <param name="cropStatusService">Service quản lý trạng thái cây trồng.</param>
    public GetAllCropStatusesHandler(ICropStatusService cropStatusService)
    {
        _cropStatusService = cropStatusService;
    }

    /// <summary>
    /// Xử lý query lấy tất cả trạng thái cây trồng.
    /// </summary>
    /// <param name="request">Query chứa điều kiện lọc.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    /// <returns>Danh sách trạng thái cây trồng.</returns>
    public async Task<IEnumerable<CropStatusDto>> Handle(GetAllCropStatusesQuery request, CancellationToken cancellationToken)
    {
        return await _cropStatusService.GetAllAsync(request.IncludeInactive);
    }
}
