// ***********************************************************************
// File: CreateCropStatusHandler.cs
// Project: FarmManagement.Application
// Mô tả: Handler xử lý command tạo mới trạng thái cây trồng.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Interfaces.Crops;
using MediatR;

namespace FarmManagement.Application.Features.CropStatuses.Commands.CreateCropStatus;

/// <summary>
/// Handler xử lý CreateCropStatusCommand.
/// </summary>
public class CreateCropStatusHandler : ICommandHandler<CreateCropStatusCommand>
{
    private readonly ICropStatusService _cropStatusService;

    /// <summary>
    /// Khởi tạo CreateCropStatusHandler.
    /// </summary>
    /// <param name="cropStatusService">Service quản lý trạng thái cây trồng.</param>
    public CreateCropStatusHandler(ICropStatusService cropStatusService)
    {
        _cropStatusService = cropStatusService;
    }

    /// <summary>
    /// Xử lý command tạo mới trạng thái cây trồng.
    /// </summary>
    /// <param name="request">Command chứa thông tin trạng thái cây trồng cần tạo.</param>
    /// <param name="cancellationToken">Token hủy bỏ.</param>
    public async Task<Unit> Handle(CreateCropStatusCommand request, CancellationToken cancellationToken)
    {
        var dto = new CreateCropStatusDto
        {
            Code = request.Code,
            Name = request.Name,
            Description = request.Description
        };

        await _cropStatusService.CreateAsync(dto);

        return Unit.Value;
    }
}
