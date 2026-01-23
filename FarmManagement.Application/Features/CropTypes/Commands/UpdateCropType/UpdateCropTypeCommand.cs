// ***********************************************************************
// File: UpdateCropTypeCommand.cs
// Project: FarmManagement.Application
// Mô tả: Command để cập nhật loại cây trồng.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropTypes.Commands.UpdateCropType;

/// <summary>
/// Command cập nhật thông tin loại cây trồng.
/// </summary>
/// <param name="Id">ID của loại cây trồng cần cập nhật.</param>
/// <param name="Code">Mã loại cây trồng (tùy chọn).</param>
/// <param name="Name">Tên loại cây trồng. Bắt buộc.</param>
/// <param name="Description">Mô tả loại cây trồng (tùy chọn).</param>
/// <param name="IsActive">Trạng thái hoạt động.</param>
public record UpdateCropTypeCommand(
    int Id,
    string? Code,
    string Name,
    string? Description,
    bool IsActive
) : ICommand;
