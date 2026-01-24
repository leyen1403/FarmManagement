using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Features.LivestockStatuses.Commands.CreateLivestockStatus;
using FarmManagement.Application.Features.LivestockStatuses.Commands.DeleteLivestockStatus;
using FarmManagement.Application.Features.LivestockStatuses.Commands.UpdateLivestockStatus;
using FarmManagement.Application.Features.LivestockStatuses.Queries.GetAllLivestockStatuses;
using FarmManagement.Application.Features.LivestockStatuses.Queries.GetLivestockStatusById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Livestocks;

/// <summary>
/// Controller quản lý trạng thái vật nuôi (LivestockStatus).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LivestockStatusesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LivestockStatusesController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Lấy tất cả trạng thái vật nuôi.
    /// </summary>
    /// <param name="includeInactive">Bao gồm trạng thái không hoạt động.</param>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
    {
        var query = new GetAllLivestockStatusesQuery(includeInactive);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Lấy trạng thái vật nuôi theo ID.
    /// </summary>
    /// <param name="id">ID của trạng thái vật nuôi.</param>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetLivestockStatusByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = "Trạng thái vật nuôi không tồn tại." });

        return Ok(result);
    }

    /// <summary>
    /// Tạo mới trạng thái vật nuôi.
    /// </summary>
    /// <param name="dto">Thông tin trạng thái vật nuôi cần tạo.</param>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLivestockStatusDto dto)
    {
        var command = new CreateLivestockStatusCommand(dto);
        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = newId }, null);
    }

    /// <summary>
    /// Cập nhật trạng thái vật nuôi.
    /// </summary>
    /// <param name="id">ID của trạng thái vật nuôi.</param>
    /// <param name="dto">Thông tin cập nhật.</param>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateLivestockStatusDto dto)
    {
        dto.Id = id;
        var command = new UpdateLivestockStatusCommand(id, dto);
        await _mediator.Send(command);
        return NoContent();
    }
    /// <summary>
    /// Xóa trạng thái vật nuôi.
    /// </summary>
    /// <param name="id">ID của trạng thái vật nuôi cần xóa.</param>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteLivestockStatusCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
