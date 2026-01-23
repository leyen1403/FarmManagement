// ***********************************************************************
// File: GetAllLocationStatusesHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý query lấy tất cả trạng thái Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Application.Interfaces.Locations;

namespace FarmManagement.Application.Features.LocationStatuses.Queries.GetAllLocationStatuses;

/// <summary>
/// Handler xử lý GetAllLocationStatusesQuery.
/// </summary>
public class GetAllLocationStatusesHandler : IQueryHandler<GetAllLocationStatusesQuery, IEnumerable<LocationStatusDto>>
{
    private readonly ILocationStatusService _locationStatusService;

    /// <summary>
    /// Khởi tạo GetAllLocationStatusesHandler.
    /// </summary>
    /// <param name="locationStatusService">Service quản lý trạng thái Location.</param>
    public GetAllLocationStatusesHandler(ILocationStatusService locationStatusService)
    {
        _locationStatusService = locationStatusService;
    }

    /// <summary>
    /// Xử lý query lấy tất cả trạng thái Location.
    /// </summary>
    /// <param name="request">Query request.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    /// <returns>Danh sách trạng thái Location.</returns>
    public async Task<IEnumerable<LocationStatusDto>> Handle(GetAllLocationStatusesQuery request, CancellationToken cancellationToken)
    {
        return await _locationStatusService.GetAllAsync();
    }
}
