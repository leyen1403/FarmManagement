using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Features.LivestockCareLogs.Commands.CreateLivestockCareLog;
using FarmManagement.Application.Features.LivestockCareLogs.Commands.DeleteLivestockCareLog;
using FarmManagement.Application.Features.LivestockCareLogs.Commands.UpdateLivestockCareLog;
using FarmManagement.Application.Features.LivestockCareLogs.Queries.GetLivestockCareLogById;
using FarmManagement.Application.Features.LivestockCareLogs.Queries.GetLivestockCareLogsByLivestockId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Livestocks;

/// <summary>
/// Controller quản lý nhật ký chăm sóc vật nuôi (LivestockCareLog).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LivestockCareLogsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LivestockCareLogsController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Lấy tất cả nhật ký chăm sóc của một vật nuôi.
    /// </summary>
    /// <param name="livestockId">ID của vật nuôi.</param>
    [HttpGet("livestock/{livestockId:int}")]
    public async Task<IActionResult> GetByLivestockId(int livestockId)
    {
        var query = new GetLivestockCareLogsByLivestockIdQuery(livestockId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Lấy nhật ký chăm sóc theo ID.
    /// </summary>
    /// <param name="id">ID của nhật ký chăm sóc.</param>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetLivestockCareLogByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = "Nhật ký chăm sóc không tồn tại." });

        return Ok(result);
    }

    /// <summary>
    /// Tạo mới nhật ký chăm sóc vật nuôi.
    /// </summary>
    /// <param name="dto">Thông tin nhật ký chăm sóc cần tạo.</param>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLivestockCareLogDto dto)
    {
        var command = new CreateLivestockCareLogCommand(dto);
        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = newId }, null);
    }

    /// <summary>
    /// Cập nhật nhật ký chăm sóc vật nuôi.
    /// </summary>
    /// <param name="id">ID của nhật ký chăm sóc.</param>
    /// <param name="dto">Thông tin cập nhật.</param>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateLivestockCareLogDto dto)
    {
        dto.Id = id;
        var command = new UpdateLivestockCareLogCommand(id, dto);
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Xóa nhật ký chăm sóc vật nuôi.
    /// </summary>
    /// <param name="id">ID của nhật ký chăm sóc cần xóa.</param>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteLivestockCareLogCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
