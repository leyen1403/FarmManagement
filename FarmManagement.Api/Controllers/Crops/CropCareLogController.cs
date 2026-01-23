using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Features.CropCareLogs.Commands.CreateCropCareLog;
using FarmManagement.Application.Features.CropCareLogs.Commands.DeleteCropCareLog;
using FarmManagement.Application.Features.CropCareLogs.Commands.UpdateCropCareLog;
using FarmManagement.Application.Features.CropCareLogs.Queries.GetCropCareLogById;
using FarmManagement.Application.Features.CropCareLogs.Queries.GetCropCareLogsByCropId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Crops;

/// <summary>
/// Controller quản lý nhật ký chăm sóc cây trồng.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CropCareLogController : ControllerBase
{
    private readonly IMediator _mediator;

    public CropCareLogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy danh sách nhật ký chăm sóc theo cây trồng.
    /// </summary>
    [HttpGet("crop/{cropId:int}")]
    public async Task<ActionResult<IEnumerable<CropCareLogDto>>> GetByCropId(int cropId)
    {
        var query = new GetCropCareLogsByCropIdQuery(cropId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Lấy nhật ký chăm sóc theo ID.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CropCareLogDto>> GetById(int id)
    {
        var query = new GetCropCareLogByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = "Nhật ký chăm sóc không tồn tại" });

        return Ok(result);
    }

    /// <summary>
    /// Tạo mới nhật ký chăm sóc.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCropCareLogDto dto)
    {
        var command = new CreateCropCareLogCommand(
            dto.CropId,
            dto.CropCareTypeId,
            dto.CareDate,
            dto.Description,
            dto.Cost
        );

        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = newId }, new { id = newId });
    }

    /// <summary>
    /// Cập nhật nhật ký chăm sóc.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCropCareLogDto dto)
    {
        if (id != dto.Id)
            return BadRequest(new { message = "ID không khớp" });

        var command = new UpdateCropCareLogCommand(
            id,
            dto.CropId,
            dto.CropCareTypeId,
            dto.CareDate,
            dto.Description,
            dto.Cost
        );

        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Xóa nhật ký chăm sóc.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteCropCareLogCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
