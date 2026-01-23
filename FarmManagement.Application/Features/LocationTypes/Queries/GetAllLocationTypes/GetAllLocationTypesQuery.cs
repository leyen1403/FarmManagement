// ***********************************************************************
// File: GetAllLocationTypesQuery.cs
// Project: FarmManagement.Application
// Mô tả: Query để lấy tất cả loại Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;

namespace FarmManagement.Application.Features.LocationTypes.Queries.GetAllLocationTypes;

/// <summary>
/// Query lấy tất cả loại Location.
/// </summary>
public record GetAllLocationTypesQuery() : IQuery<IEnumerable<LocationTypeDto>>;
