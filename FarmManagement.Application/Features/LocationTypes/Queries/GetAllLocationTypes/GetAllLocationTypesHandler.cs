// ***********************************************************************
// File: GetAllLocationTypesHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý query lấy tất cả loại Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Application.Interfaces.Locations;

namespace FarmManagement.Application.Features.LocationTypes.Queries.GetAllLocationTypes;

/// <summary>
/// Handler xử lý GetAllLocationTypesQuery.
/// </summary>
public class GetAllLocationTypesHandler : IQueryHandler<GetAllLocationTypesQuery, IEnumerable<LocationTypeDto>>
{
    private readonly ILocationTypeService _locationTypeService;

    /// <summary>
    /// Khởi tạo GetAllLocationTypesHandler.
    /// </summary>
    /// <param name="locationTypeService">Service quản lý loại Location.</param>
    public GetAllLocationTypesHandler(ILocationTypeService locationTypeService)
    {
        _locationTypeService = locationTypeService;
    }

    /// <summary>
    /// Xử lý query lấy tất cả loại Location.
    /// </summary>
    /// <param name="request">Query request.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    /// <returns>Danh sách loại Location.</returns>
    public async Task<IEnumerable<LocationTypeDto>> Handle(GetAllLocationTypesQuery request, CancellationToken cancellationToken)
    {
        return await _locationTypeService.GetAllAsync();
    }
}
