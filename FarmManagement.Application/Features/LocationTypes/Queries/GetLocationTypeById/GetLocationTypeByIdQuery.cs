// ***********************************************************************
// File: GetLocationTypeByIdQuery.cs
// Project: FarmManagement.Application
// Mô tả: Query để lấy loại Location theo ID.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;

namespace FarmManagement.Application.Features.LocationTypes.Queries.GetLocationTypeById;

/// <summary>
/// Query lấy loại Location theo ID.
/// </summary>
/// <param name="Id">ID của loại Location cần lấy.</param>
public record GetLocationTypeByIdQuery(int Id) : IQuery<LocationTypeDto>;
