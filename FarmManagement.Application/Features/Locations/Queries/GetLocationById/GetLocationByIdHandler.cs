// ***********************************************************************
// File: GetLocationByIdHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý query lấy Location theo ID.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Application.Interfaces.Locations;

namespace FarmManagement.Application.Features.Locations.Queries.GetLocationById;

/// <summary>
/// Handler xử lý GetLocationByIdQuery.
/// </summary>
public class GetLocationByIdHandler : IQueryHandler<GetLocationByIdQuery, LocationDto>
{
    private readonly ILocationService _locationService;

    /// <summary>
    /// Khởi tạo GetLocationByIdHandler.
    /// </summary>
    /// <param name="locationService">Service quản lý Location.</param>
    public GetLocationByIdHandler(ILocationService locationService)
    {
        _locationService = locationService;
    }

    /// <summary>
    /// Xử lý query lấy Location theo ID.
    /// </summary>
    /// <param name="request">Query chứa ID Location.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    /// <returns>Location tìm được.</returns>
    public async Task<LocationDto> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
    {
        return await _locationService.GetByIdAsync(request.Id);
    }
}
