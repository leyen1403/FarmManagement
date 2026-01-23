// ***********************************************************************
// File: DeleteCropStatusCommand.cs
// Project: FarmManagement.Application
// Mô tả: Command để xóa trạng thái cây trồng.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropStatuses.Commands.DeleteCropStatus;

/// <summary>
/// Command xóa trạng thái cây trồng theo ID.
/// </summary>
/// <param name="Id">ID của trạng thái cây trồng cần xóa.</param>
public record DeleteCropStatusCommand(int Id) : ICommand;
