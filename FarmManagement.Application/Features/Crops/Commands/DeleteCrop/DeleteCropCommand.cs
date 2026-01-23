using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.Crops.Commands.DeleteCrop;

/// <summary>
/// Command xóa cây trồng.
/// </summary>
public record DeleteCropCommand(int Id) : ICommand;
