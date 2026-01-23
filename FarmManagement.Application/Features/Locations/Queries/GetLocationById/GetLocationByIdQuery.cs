// ***********************************************************************
// File: GetLocationByIdQuery.cs
// Project: FarmManagement.Application
// Mô tả: Query để lấy Location theo ID.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;

namespace FarmManagement.Application.Features.Locations.Queries.GetLocationById;

/// <summary>
/// Query lấy Location theo ID.
/// </summary>
/// <param name="Id">ID của Location cần lấy.</param>
public record GetLocationByIdQuery(int Id) : IQuery<LocationDto>;
