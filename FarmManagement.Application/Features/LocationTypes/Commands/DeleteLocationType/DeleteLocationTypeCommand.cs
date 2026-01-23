// ***********************************************************************
// File: DeleteLocationTypeCommand.cs
// Project: FarmManagement.Application
// Mô tả: Command để xóa loại Location.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.LocationTypes.Commands.DeleteLocationType;

/// <summary>
/// Command xóa loại Location theo ID.
/// </summary>
/// <param name="Id">ID của loại Location cần xóa.</param>
public record DeleteLocationTypeCommand(int Id) : ICommand;
