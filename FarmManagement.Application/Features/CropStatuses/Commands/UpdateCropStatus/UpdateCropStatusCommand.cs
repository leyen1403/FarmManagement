// ***********************************************************************
// File: UpdateCropStatusCommand.cs
// Project: FarmManagement.Application
// Mô tả: Command để cập nhật trạng thái cây trồng.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropStatuses.Commands.UpdateCropStatus;

/// <summary>
/// Command cập nhật thông tin trạng thái cây trồng.
/// </summary>
/// <param name="Id">ID của trạng thái cây trồng cần cập nhật.</param>
/// <param name="Code">Mã trạng thái cây trồng. Bắt buộc.</param>
/// <param name="Name">Tên trạng thái cây trồng. Bắt buộc.</param>
/// <param name="Description">Mô tả trạng thái cây trồng (tùy chọn).</param>
/// <param name="IsActive">Trạng thái hoạt động.</param>
public record UpdateCropStatusCommand(
    int Id,
    string Code,
    string Name,
    string? Description,
    bool IsActive
) : ICommand;
