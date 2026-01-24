using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Features.LivestockHealthLogs.Commands.CreateLivestockHealthLog;
using FarmManagement.Application.Features.LivestockHealthLogs.Commands.DeleteLivestockHealthLog;
using FarmManagement.Application.Features.LivestockHealthLogs.Commands.UpdateLivestockHealthLog;
using FarmManagement.Application.Features.LivestockHealthLogs.Queries.GetLivestockHealthLogById;
using FarmManagement.Application.Features.LivestockHealthLogs.Queries.GetLivestockHealthLogsByLivestockId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Livestocks;

/// <summary>
/// Controller quản lý nhật ký sức khỏe vật nuôi (LivestockHealthLog).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LivestockHealthLogsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LivestockHealthLogsController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Lấy tất cả nhật ký sức khỏe của một vật nuôi.
    /// </summary>
    /// <param name="livestockId">ID của vật nuôi.</param>
    [HttpGet("livestock/{livestockId:int}")]
    public async Task<IActionResult> GetByLivestockId(int livestockId)
    {
        var query = new GetLivestockHealthLogsByLivestockIdQuery(livestockId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Lấy nhật ký sức khỏe theo ID.
    /// </summary>
    /// <param name="id">ID của nhật ký sức khỏe.</param>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetLivestockHealthLogByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = "Nhật ký sức khỏe không tồn tại." });

        return Ok(result);
    }

    /// <summary>
    /// Tạo mới nhật ký sức khỏe vật nuôi.
    /// </summary>
    /// <param name="dto">Thông tin nhật ký sức khỏe cần tạo.</param>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLivestockHealthLogDto dto)
    {
        var command = new CreateLivestockHealthLogCommand(dto);
        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = newId }, null);
    }

    /// <summary>
    /// Cập nhật nhật ký sức khỏe vật nuôi.
    /// </summary>
    /// <param name="id">ID của nhật ký sức khỏe.</param>
    /// <param name="dto">Thông tin cập nhật.</param>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateLivestockHealthLogDto dto)
    {
        dto.Id = id;
        var command = new UpdateLivestockHealthLogCommand(id, dto);
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Xóa nhật ký sức khỏe vật nuôi.
    /// </summary>
    /// <param name="id">ID của nhật ký sức khỏe cần xóa.</param>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteLivestockHealthLogCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
