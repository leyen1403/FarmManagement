// ***********************************************************************
// File: GetAllLocationsHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý query lấy tất cả Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Application.Interfaces.Locations;

namespace FarmManagement.Application.Features.Locations.Queries.GetAllLocations;

/// <summary>
/// Handler xử lý GetAllLocationsQuery.
/// </summary>
public class GetAllLocationsHandler : IQueryHandler<GetAllLocationsQuery, IEnumerable<LocationDto>>
{
    private readonly ILocationService _locationService;

    /// <summary>
    /// Khởi tạo GetAllLocationsHandler.
    /// </summary>
    /// <param name="locationService">Service quản lý Location.</param>
    public GetAllLocationsHandler(ILocationService locationService)
    {
        _locationService = locationService;
    }

    /// <summary>
    /// Xử lý query lấy tất cả Location.
    /// </summary>
    /// <param name="request">Query request.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    /// <returns>Danh sách Location.</returns>
    public async Task<IEnumerable<LocationDto>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
    {
        return await _locationService.GetAllAsync(request.ActiveOnly);
    }
}
