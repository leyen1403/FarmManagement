// ***********************************************************************
// File: CreateLocationTypeCommand.cs
// Project: FarmManagement.Application
// Mô tả: Command để tạo mới loại Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Locations;

namespace FarmManagement.Application.Features.LocationTypes.Commands.CreateLocationType;

/// <summary>
/// Command tạo mới một loại Location.
/// </summary>
/// <param name="Code">Mã loại Location. Bắt buộc.</param>
/// <param name="Name">Tên loại Location. Bắt buộc.</param>
public record CreateLocationTypeCommand(
    string Code,
    string Name
) : ICommand<LocationTypeDto>;
