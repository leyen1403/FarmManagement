// ***********************************************************************
// File: GetAllLocationStatusesQuery.cs
// Project: FarmManagement.Application
// Mô tả: Query để lấy tất cả trạng thái Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;

namespace FarmManagement.Application.Features.LocationStatuses.Queries.GetAllLocationStatuses;

/// <summary>
/// Query lấy tất cả trạng thái Location.
/// </summary>
public record GetAllLocationStatusesQuery() : IQuery<IEnumerable<LocationStatusDto>>;
