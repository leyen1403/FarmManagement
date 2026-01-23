// ***********************************************************************
// File: GetLocationStatusByIdQuery.cs
// Project: FarmManagement.Application
// Mô tả: Query để lấy trạng thái Location theo ID.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;

namespace FarmManagement.Application.Features.LocationStatuses.Queries.GetLocationStatusById;

/// <summary>
/// Query lấy trạng thái Location theo ID.
/// </summary>
/// <param name="Id">ID của trạng thái Location cần lấy.</param>
public record GetLocationStatusByIdQuery(int Id) : IQuery<LocationStatusDto>;
