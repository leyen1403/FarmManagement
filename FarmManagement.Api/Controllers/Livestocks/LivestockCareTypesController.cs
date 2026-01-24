using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Features.LivestockCareTypes.Commands.CreateLivestockCareType;
using FarmManagement.Application.Features.LivestockCareTypes.Commands.DeleteLivestockCareType;
using FarmManagement.Application.Features.LivestockCareTypes.Commands.UpdateLivestockCareType;
using FarmManagement.Application.Features.LivestockCareTypes.Queries.GetAllLivestockCareTypes;
using FarmManagement.Application.Features.LivestockCareTypes.Queries.GetLivestockCareTypeById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Livestocks;

/// <summary>
/// Controller quản lý loại chăm sóc vật nuôi (LivestockCareType).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LivestockCareTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LivestockCareTypesController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Lấy tất cả loại chăm sóc vật nuôi.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllLivestockCareTypesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Lấy loại chăm sóc vật nuôi theo ID.
    /// </summary>
    /// <param name="id">ID của loại chăm sóc.</param>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetLivestockCareTypeByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = "Loại chăm sóc vật nuôi không tồn tại." });

        return Ok(result);
    }

    /// <summary>
    /// Tạo mới loại chăm sóc vật nuôi.
    /// </summary>
    /// <param name="dto">Thông tin loại chăm sóc cần tạo.</param>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLivestockCareTypeDto dto)
    {
        var command = new CreateLivestockCareTypeCommand(dto);
        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = newId }, null);
    }

    /// <summary>
    /// Cập nhật loại chăm sóc vật nuôi.
    /// </summary>
    /// <param name="id">ID của loại chăm sóc.</param>
    /// <param name="dto">Thông tin cập nhật.</param>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateLivestockCareTypeDto dto)
    {
        dto.Id = id;
        var command = new UpdateLivestockCareTypeCommand(id, dto);
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Xóa loại chăm sóc vật nuôi.
    /// </summary>
    /// <param name="id">ID của loại chăm sóc cần xóa.</param>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteLivestockCareTypeCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
