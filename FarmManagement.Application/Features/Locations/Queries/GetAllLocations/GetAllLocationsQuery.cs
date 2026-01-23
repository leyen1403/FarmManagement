// ***********************************************************************
// File: GetAllLocationsQuery.cs
// Project: FarmManagement.Application
// Mô tả: Query để lấy tất cả Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;

namespace FarmManagement.Application.Features.Locations.Queries.GetAllLocations;

/// <summary>
/// Query lấy tất cả Location.
/// </summary>
/// <param name="ActiveOnly">Nếu true, chỉ lấy các Location đang hoạt động.</param>
public record GetAllLocationsQuery(bool ActiveOnly = false) : IQuery<IEnumerable<LocationDto>>;
