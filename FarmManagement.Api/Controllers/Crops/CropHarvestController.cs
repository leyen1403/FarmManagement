using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Features.CropHarvests.Commands.CreateCropHarvest;
using FarmManagement.Application.Features.CropHarvests.Commands.DeleteCropHarvest;
using FarmManagement.Application.Features.CropHarvests.Commands.UpdateCropHarvest;
using FarmManagement.Application.Features.CropHarvests.Queries.GetCropHarvestById;
using FarmManagement.Application.Features.CropHarvests.Queries.GetCropHarvestsByCropId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Crops;

/// <summary>
/// Controller quản lý thu hoạch cây trồng.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CropHarvestController : ControllerBase
{
    private readonly IMediator _mediator;

    public CropHarvestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy danh sách thu hoạch theo cây trồng.
    /// </summary>
    [HttpGet("crop/{cropId:int}")]
    public async Task<ActionResult<IEnumerable<CropHarvestDto>>> GetByCropId(int cropId)
    {
        var query = new GetCropHarvestsByCropIdQuery(cropId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Lấy thu hoạch theo ID.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CropHarvestDto>> GetById(int id)
    {
        var query = new GetCropHarvestByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = "Thu hoạch không tồn tại" });

        return Ok(result);
    }

    /// <summary>
    /// Tạo mới thu hoạch.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCropHarvestDto dto)
    {
        var command = new CreateCropHarvestCommand(
            dto.CropId,
            dto.HarvestDate,
            dto.Quantity,
            dto.Unit,
            dto.UnitPrice,
            dto.Buyer,
            dto.Note
        );

        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = newId }, new { id = newId });
    }

    /// <summary>
    /// Cập nhật thu hoạch.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCropHarvestDto dto)
    {
        if (id != dto.Id)
            return BadRequest(new { message = "ID không khớp" });

        var command = new UpdateCropHarvestCommand(
            id,
            dto.CropId,
            dto.HarvestDate,
            dto.Quantity,
            dto.Unit,
            dto.UnitPrice,
            dto.Buyer,
            dto.Note
        );

        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Xóa thu hoạch.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteCropHarvestCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
