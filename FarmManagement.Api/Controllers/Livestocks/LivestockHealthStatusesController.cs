using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Features.LivestockHealthStatuses.Commands.CreateLivestockHealthStatus;
using FarmManagement.Application.Features.LivestockHealthStatuses.Commands.DeleteLivestockHealthStatus;
using FarmManagement.Application.Features.LivestockHealthStatuses.Commands.UpdateLivestockHealthStatus;
using FarmManagement.Application.Features.LivestockHealthStatuses.Queries.GetAllLivestockHealthStatuses;
using FarmManagement.Application.Features.LivestockHealthStatuses.Queries.GetLivestockHealthStatusById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Livestocks;

/// <summary>
/// Controller quản lý trạng thái sức khỏe vật nuôi (LivestockHealthStatus).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LivestockHealthStatusesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LivestockHealthStatusesController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Lấy tất cả trạng thái sức khỏe vật nuôi.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllLivestockHealthStatusesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Lấy trạng thái sức khỏe vật nuôi theo ID.
    /// </summary>
    /// <param name="id">ID của trạng thái sức khỏe.</param>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetLivestockHealthStatusByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = "Trạng thái sức khỏe vật nuôi không tồn tại." });

        return Ok(result);
    }

    /// <summary>
    /// Tạo mới trạng thái sức khỏe vật nuôi.
    /// </summary>
    /// <param name="dto">Thông tin trạng thái sức khỏe cần tạo.</param>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLivestockHealthStatusDto dto)
    {
        var command = new CreateLivestockHealthStatusCommand(dto);
        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = newId }, null);
    }

    /// <summary>
    /// Cập nhật trạng thái sức khỏe vật nuôi.
    /// </summary>
    /// <param name="id">ID của trạng thái sức khỏe.</param>
    /// <param name="dto">Thông tin cập nhật.</param>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateLivestockHealthStatusDto dto)
    {
        dto.Id = id;
        var command = new UpdateLivestockHealthStatusCommand(id, dto);
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Xóa trạng thái sức khỏe vật nuôi.
    /// </summary>
    /// <param name="id">ID của trạng thái sức khỏe cần xóa.</param>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteLivestockHealthStatusCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
