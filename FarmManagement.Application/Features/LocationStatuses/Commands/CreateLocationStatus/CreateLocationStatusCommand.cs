// ***********************************************************************
// File: CreateLocationStatusCommand.cs
// Project: FarmManagement.Application
// Mô tả: Command để tạo mới trạng thái Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;

namespace FarmManagement.Application.Features.LocationStatuses.Commands.CreateLocationStatus;

/// <summary>
/// Command tạo mới một trạng thái Location.
/// </summary>
/// <param name="Code">Mã trạng thái Location. Bắt buộc.</param>
/// <param name="Name">Tên trạng thái Location. Bắt buộc.</param>
public record CreateLocationStatusCommand(
    string Code,
    string Name
) : ICommand<LocationStatusDto>;
