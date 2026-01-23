// ***********************************************************************
// File: UpdateLocationStatusCommand.cs
// Project: FarmManagement.Application
// Mô tả: Command để cập nhật trạng thái Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.LocationStatuses.Commands.UpdateLocationStatus;

/// <summary>
/// Command cập nhật thông tin trạng thái Location.
/// </summary>
/// <param name="Id">ID của trạng thái Location cần cập nhật.</param>
/// <param name="Code">Mã trạng thái Location. Bắt buộc.</param>
/// <param name="Name">Tên trạng thái Location. Bắt buộc.</param>
public record UpdateLocationStatusCommand(
    int Id,
    string Code,
    string Name
) : ICommand;
