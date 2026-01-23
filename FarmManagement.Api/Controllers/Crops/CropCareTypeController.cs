using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Features.CropCareTypes.Commands.CreateCropCareType;
using FarmManagement.Application.Features.CropCareTypes.Commands.DeleteCropCareType;
using FarmManagement.Application.Features.CropCareTypes.Commands.UpdateCropCareType;
using FarmManagement.Application.Features.CropCareTypes.Queries.GetAllCropCareTypes;
using FarmManagement.Application.Features.CropCareTypes.Queries.GetCropCareTypeById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Crops;

/// <summary>
/// Controller quản lý loại chăm sóc cây trồng.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CropCareTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public CropCareTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy tất cả loại chăm sóc.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CropCareTypeDto>>> GetAll([FromQuery] bool includeInactive = false)
    {
        var query = new GetAllCropCareTypesQuery(includeInactive);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Lấy loại chăm sóc theo ID.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CropCareTypeDto>> GetById(int id)
    {
        var query = new GetCropCareTypeByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = "Loại chăm sóc không tồn tại" });

        return Ok(result);
    }

    /// <summary>
    /// Tạo mới loại chăm sóc.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCropCareTypeDto dto)
    {
        var command = new CreateCropCareTypeCommand(dto.Code, dto.Name, dto.Description);
        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = newId }, new { id = newId });
    }

    /// <summary>
    /// Cập nhật loại chăm sóc.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCropCareTypeDto dto)
    {
        if (id != dto.Id)
            return BadRequest(new { message = "ID không khớp" });

        var command = new UpdateCropCareTypeCommand(id, dto.Code, dto.Name, dto.Description, dto.IsActive);
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Xóa loại chăm sóc.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteCropCareTypeCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
