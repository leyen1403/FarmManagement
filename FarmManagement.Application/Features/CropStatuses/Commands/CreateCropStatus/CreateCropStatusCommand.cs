// ***********************************************************************
// File: CreateCropStatusCommand.cs
// Project: FarmManagement.Application
// Mô tả: Command để tạo mới trạng thái cây trồng.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropStatuses.Commands.CreateCropStatus;

/// <summary>
/// Command tạo mới một trạng thái cây trồng.
/// </summary>
/// <param name="Code">Mã trạng thái cây trồng. Bắt buộc.</param>
/// <param name="Name">Tên trạng thái cây trồng. Bắt buộc.</param>
/// <param name="Description">Mô tả trạng thái cây trồng (tùy chọn).</param>
public record CreateCropStatusCommand(
    string Code,
    string Name,
    string? Description
) : ICommand;
