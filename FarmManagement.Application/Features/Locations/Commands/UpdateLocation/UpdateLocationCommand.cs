// ***********************************************************************
// File: UpdateLocationCommand.cs
// Project: FarmManagement.Application
// Mô tả: Command để cập nhật Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.Locations.Commands.UpdateLocation;

/// <summary>
/// Command cập nhật thông tin Location.
/// </summary>
public record UpdateLocationCommand(
    int Id,
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
) : ICommand;
