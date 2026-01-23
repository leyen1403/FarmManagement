// ***********************************************************************
// File: GetCropStatusByIdHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý query lấy trạng thái cây trồng theo ID.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;

namespace FarmManagement.Application.Features.CropStatuses.Queries.GetCropStatusById;

/// <summary>
/// Handler xử lý GetCropStatusByIdQuery.
/// </summary>
public class GetCropStatusByIdHandler : IQueryHandler<GetCropStatusByIdQuery, CropStatusDto>
{
    private readonly ICropStatusService _cropStatusService;

    /// <summary>
    /// Khởi tạo GetCropStatusByIdHandler.
    /// </summary>
    /// <param name="cropStatusService">Service quản lý trạng thái cây trồng.</param>
    public GetCropStatusByIdHandler(ICropStatusService cropStatusService)
    {
        _cropStatusService = cropStatusService;
    }

    /// <summary>
    /// Xử lý query lấy trạng thái cây trồng theo ID.
    /// </summary>
    /// <param name="request">Query chứa ID trạng thái cây trồng.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    /// <returns>Trạng thái cây trồng tìm được.</returns>
    public async Task<CropStatusDto> Handle(GetCropStatusByIdQuery request, CancellationToken cancellationToken)
    {
        return await _cropStatusService.GetByIdAsync(request.Id);
    }
}
