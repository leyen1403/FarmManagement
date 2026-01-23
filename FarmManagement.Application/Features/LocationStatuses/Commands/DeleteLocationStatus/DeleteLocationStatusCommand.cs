// ***********************************************************************
// File: DeleteLocationStatusCommand.cs
// Project: FarmManagement.Application
// Mô tả: Command để xóa trạng thái Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.LocationStatuses.Commands.DeleteLocationStatus;

/// <summary>
/// Command xóa trạng thái Location theo ID.
/// </summary>
/// <param name="Id">ID của trạng thái Location cần xóa.</param>
public record DeleteLocationStatusCommand(int Id) : ICommand;
