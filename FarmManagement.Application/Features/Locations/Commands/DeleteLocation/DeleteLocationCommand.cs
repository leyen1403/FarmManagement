// ***********************************************************************
// File: DeleteLocationCommand.cs
// Project: FarmManagement.Application
// Mô tả: Command để xóa Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.Locations.Commands.DeleteLocation;

/// <summary>
/// Command xóa Location theo ID.
/// </summary>
/// <param name="Id">ID của Location cần xóa.</param>
public record DeleteLocationCommand(int Id) : ICommand;
