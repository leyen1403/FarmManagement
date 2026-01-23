// ***********************************************************************
// File: DeleteCropTypeCommand.cs
// Project: FarmManagement.Application
// Mô tả: Command để xóa loại cây trồng.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropTypes.Commands.DeleteCropType;

/// <summary>
/// Command xóa loại cây trồng theo ID.
/// </summary>
/// <param name="Id">ID của loại cây trồng cần xóa.</param>
public record DeleteCropTypeCommand(int Id) : ICommand;
