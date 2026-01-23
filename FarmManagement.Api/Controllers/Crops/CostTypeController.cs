using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Features.CostTypes.Commands.CreateCostType;
using FarmManagement.Application.Features.CostTypes.Commands.DeleteCostType;
using FarmManagement.Application.Features.CostTypes.Commands.UpdateCostType;
using FarmManagement.Application.Features.CostTypes.Queries.GetAllCostTypes;
using FarmManagement.Application.Features.CostTypes.Queries.GetCostTypeById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Crops;

/// <summary>
/// Controller quản lý loại chi phí.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CostTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public CostTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy tất cả loại chi phí.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CostTypeDto>>> GetAll([FromQuery] bool includeInactive = false)
    {
        var query = new GetAllCostTypesQuery(includeInactive);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Lấy loại chi phí theo ID.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CostTypeDto>> GetById(int id)
    {
        var query = new GetCostTypeByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = "Loại chi phí không tồn tại" });

        return Ok(result);
    }

    /// <summary>
    /// Tạo mới loại chi phí.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCostTypeDto dto)
    {
        var command = new CreateCostTypeCommand(dto.Code, dto.Name, dto.Description);
        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = newId }, new { id = newId });
    }

    /// <summary>
    /// Cập nhật loại chi phí.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCostTypeDto dto)
    {
        if (id != dto.Id)
            return BadRequest(new { message = "ID không khớp" });

        var command = new UpdateCostTypeCommand(id, dto.Code, dto.Name, dto.Description, dto.IsActive);
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Xóa loại chi phí.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteCostTypeCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
