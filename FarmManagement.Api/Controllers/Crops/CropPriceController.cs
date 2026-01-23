using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Features.CropPrices.Commands.CreateCropPrice;
using FarmManagement.Application.Features.CropPrices.Commands.DeleteCropPrice;
using FarmManagement.Application.Features.CropPrices.Commands.UpdateCropPrice;
using FarmManagement.Application.Features.CropPrices.Queries.GetAllCropPrices;
using FarmManagement.Application.Features.CropPrices.Queries.GetCropPriceById;
using FarmManagement.Application.Features.CropPrices.Queries.GetCropPriceHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Crops;

/// <summary>
/// Controller quản lý giá cây trồng (CropPrice).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CropPriceController : ControllerBase
{
    private readonly IMediator _mediator;

    public CropPriceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy tất cả giá cây trồng.
    /// </summary>
    /// <param name="cropId">Lọc theo cây trồng.</param>
    /// <param name="includeInactive">Bao gồm giá không hoạt động.</param>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CropPriceDto>>> GetAll(
        [FromQuery] int? cropId = null,
        [FromQuery] bool includeInactive = false)
    {
        var query = new GetAllCropPricesQuery(cropId, includeInactive);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Lấy giá cây trồng theo ID.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CropPriceDto>> GetById(int id)
    {
        var query = new GetCropPriceByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound(new { message = "Giá cây trồng không tồn tại" });
        }

        return Ok(result);
    }

    /// <summary>
    /// Lấy lịch sử giá của một cây trồng.
    /// </summary>
    [HttpGet("history/{cropId:int}")]
    public async Task<ActionResult<IEnumerable<CropPriceDto>>> GetPriceHistory(int cropId)
    {
        var query = new GetCropPriceHistoryQuery(cropId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Tạo mới giá cây trồng.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCropPriceDto dto)
    {
        var command = new CreateCropPriceCommand(
            dto.CropId,
            dto.Price,
            dto.Unit,
            dto.EffectiveDate,
            dto.ExpiryDate,
            dto.Note
        );

        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = newId }, new { id = newId });
    }

    /// <summary>
    /// Cập nhật giá cây trồng.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCropPriceDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(new { message = "ID không khớp" });
        }

        var command = new UpdateCropPriceCommand(
            id,
            dto.CropId,
            dto.Price,
            dto.Unit,
            dto.EffectiveDate,
            dto.ExpiryDate,
            dto.Note,
            dto.IsActive
        );

        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Xóa giá cây trồng.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteCropPriceCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}