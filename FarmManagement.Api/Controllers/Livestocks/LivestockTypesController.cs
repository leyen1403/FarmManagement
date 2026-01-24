using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Features.LivestockTypes.Commands.CreateLivestockType;
using FarmManagement.Application.Features.LivestockTypes.Commands.DeleteLivestockType;
using FarmManagement.Application.Features.LivestockTypes.Commands.UpdateLivestockType;
using FarmManagement.Application.Features.LivestockTypes.Queries.GetAllLivestockTypes;
using FarmManagement.Application.Features.LivestockTypes.Queries.GetLivestockTypeById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Livestocks;

/// <summary>
/// Controller quản lý loại vật nuôi (LivestockType).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LivestockTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LivestockTypesController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Lấy tất cả loại vật nuôi.
    /// </summary>
    /// <param name="includeInactive">Bao gồm loại không hoạt động.</param>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
    {
        var query = new GetAllLivestockTypesQuery(includeInactive);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Lấy loại vật nuôi theo ID.
    /// </summary>
    /// <param name="id">ID của loại vật nuôi.</param>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetLivestockTypeByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = "Loại vật nuôi không tồn tại." });

        return Ok(result);
    }

    /// <summary>
    /// Tạo mới loại vật nuôi.
    /// </summary>
    /// <param name="dto">Thông tin loại vật nuôi cần tạo.</param>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLivestockTypeDto dto)
    {
        var command = new CreateLivestockTypeCommand(dto);
        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = newId }, null);
    }

    /// <summary>
    /// Cập nhật loại vật nuôi.
    /// </summary>
    /// <param name="id">ID của loại vật nuôi.</param>
    /// <param name="dto">Thông tin cập nhật.</param>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateLivestockTypeDto dto)
    {
        dto.Id = id;
        var command = new UpdateLivestockTypeCommand(id, dto);
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Xóa loại vật nuôi.
    /// </summary>
    /// <param name="id">ID của loại vật nuôi cần xóa.</param>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteLivestockTypeCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
