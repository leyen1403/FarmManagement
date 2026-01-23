// ***********************************************************************
// File: CreateCropTypeCommand.cs
// Project: FarmManagement.Application
// Mô tả: Command để tạo mới loại cây trồng.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.CropTypes.Commands.CreateCropType;

/// <summary>
/// Command tạo mới một loại cây trồng.
/// </summary>
/// <param name="Code">Mã loại cây trồng (tùy chọn).</param>
/// <param name="Name">Tên loại cây trồng. Bắt buộc.</param>
/// <param name="Description">Mô tả loại cây trồng (tùy chọn).</param>
public record CreateCropTypeCommand(
    string? Code,
    string Name,
    string? Description
) : ICommand<int>;
