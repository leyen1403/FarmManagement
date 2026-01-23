// ***********************************************************************
// File: UpdateLocationTypeCommand.cs
// Project: FarmManagement.Application
// Mô tả: Command để cập nhật loại Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.LocationTypes.Commands.UpdateLocationType;

/// <summary>
/// Command cập nhật thông tin loại Location.
/// </summary>
/// <param name="Id">ID của loại Location cần cập nhật.</param>
/// <param name="Code">Mã loại Location. Bắt buộc.</param>
/// <param name="Name">Tên loại Location. Bắt buộc.</param>
public record UpdateLocationTypeCommand(
    int Id,
    string Code,
    string Name
) : ICommand;
