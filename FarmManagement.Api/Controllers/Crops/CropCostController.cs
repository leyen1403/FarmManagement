using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Features.CropCosts.Commands.CreateCropCost;
using FarmManagement.Application.Features.CropCosts.Commands.DeleteCropCost;
using FarmManagement.Application.Features.CropCosts.Commands.UpdateCropCost;
using FarmManagement.Application.Features.CropCosts.Queries.GetCropCostById;
using FarmManagement.Application.Features.CropCosts.Queries.GetCropCostsByCropId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Crops;

/// <summary>
/// Controller quản lý chi phí cây trồng.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CropCostController : ControllerBase
{
    private readonly IMediator _mediator;

    public CropCostController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy danh sách chi phí theo cây trồng.
    /// </summary>
    [HttpGet("crop/{cropId:int}")]
    public async Task<ActionResult<IEnumerable<CropCostDto>>> GetByCropId(int cropId)
    {
        var query = new GetCropCostsByCropIdQuery(cropId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Lấy chi phí theo ID.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CropCostDto>> GetById(int id)
    {
        var query = new GetCropCostByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound(new { message = "Chi phí không tồn tại" });
        }

        return Ok(result);
    }

    /// <summary>
    /// Tạo mới chi phí.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCropCostDto dto)
    {
        var command = new CreateCropCostCommand(
            dto.CropId,
            dto.CostTypeId,
            dto.CostDate,
            dto.Quantity,
            dto.Unit,
            dto.UnitPrice,
            dto.Note
        );

        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = newId }, new { id = newId });
    }

    /// <summary>
    /// Cập nhật chi phí.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCropCostDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(new { message = "ID không khớp" });
        }

        var command = new UpdateCropCostCommand(
            id,
            dto.CropId,
            dto.CostTypeId,
            dto.CostDate,
            dto.Quantity,
            dto.Unit,
            dto.UnitPrice,
            dto.Note
        );

        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Xóa chi phí.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteCropCostCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}