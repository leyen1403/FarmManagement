// ***********************************************************************
// File: CreateLocationCommand.cs
// Project: FarmManagement.Application
// Mô tả: Command để tạo mới Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;

namespace FarmManagement.Application.Features.Locations.Commands.CreateLocation;

/// <summary>
/// Command tạo mới một Location.
/// </summary>
public record CreateLocationCommand(
    string Name,
    int LocationTypeId,
    int LocationStatusId,
    string? Address,
    string? Description,
    decimal? Area,
    decimal? Capacity,
    string? CapacityUnit,
    DateTime? StartDate,
    DateTime? EndDate,
    int? ParentLocationId,
    string? Note
) : ICommand<LocationDto>;
