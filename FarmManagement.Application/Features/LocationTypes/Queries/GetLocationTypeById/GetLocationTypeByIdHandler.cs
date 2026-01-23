// ***********************************************************************
// File: GetLocationTypeByIdHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý query lấy loại Location theo ID.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Application.Interfaces.Locations;

namespace FarmManagement.Application.Features.LocationTypes.Queries.GetLocationTypeById;

/// <summary>
/// Handler xử lý GetLocationTypeByIdQuery.
/// </summary>
public class GetLocationTypeByIdHandler : IQueryHandler<GetLocationTypeByIdQuery, LocationTypeDto>
{
    private readonly ILocationTypeService _locationTypeService;

    /// <summary>
    /// Khởi tạo GetLocationTypeByIdHandler.
    /// </summary>
    /// <param name="locationTypeService">Service quản lý loại Location.</param>
    public GetLocationTypeByIdHandler(ILocationTypeService locationTypeService)
    {
        _locationTypeService = locationTypeService;
    }

    /// <summary>
    /// Xử lý query lấy loại Location theo ID.
    /// </summary>
    /// <param name="request">Query chứa ID loại Location.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    /// <returns>Loại Location tìm được.</returns>
    public async Task<LocationTypeDto> Handle(GetLocationTypeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _locationTypeService.GetByIdAsync(request.Id);
    }
}
