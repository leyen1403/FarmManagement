// ***********************************************************************
// File: GetLocationStatusByIdHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý query lấy trạng thái Location theo ID.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Application.Interfaces.Locations;

namespace FarmManagement.Application.Features.LocationStatuses.Queries.GetLocationStatusById;

/// <summary>
/// Handler xử lý GetLocationStatusByIdQuery.
/// </summary>
public class GetLocationStatusByIdHandler : IQueryHandler<GetLocationStatusByIdQuery, LocationStatusDto>
{
    private readonly ILocationStatusService _locationStatusService;

    /// <summary>
    /// Khởi tạo GetLocationStatusByIdHandler.
    /// </summary>
    /// <param name="locationStatusService">Service quản lý trạng thái Location.</param>
    public GetLocationStatusByIdHandler(ILocationStatusService locationStatusService)
    {
        _locationStatusService = locationStatusService;
    }

    /// <summary>
    /// Xử lý query lấy trạng thái Location theo ID.
    /// </summary>
    /// <param name="request">Query chứa ID trạng thái Location.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    /// <returns>Trạng thái Location tìm được.</returns>
    public async Task<LocationStatusDto> Handle(GetLocationStatusByIdQuery request, CancellationToken cancellationToken)
    {
        return await _locationStatusService.GetByIdAsync(request.Id);
    }
}
