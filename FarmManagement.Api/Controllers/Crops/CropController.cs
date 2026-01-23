using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Features.Crops.Commands.CreateCrop;
using FarmManagement.Application.Features.Crops.Commands.DeleteCrop;
using FarmManagement.Application.Features.Crops.Commands.UpdateCrop;
using FarmManagement.Application.Features.Crops.Queries.GetAllCrops;
using FarmManagement.Application.Features.Crops.Queries.GetCropById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Crops;

/// <summary>
/// Controller quản lý cây trồng (Crop).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CropController : ControllerBase
{
    private readonly IMediator _mediator;

    public CropController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy tất cả cây trồng.
    /// </summary>
    /// <param name="cropTypeId">Lọc theo loại cây trồng.</param>
    /// <param name="cropStatusId">Lọc theo trạng thái.</param>
    /// <param name="locationId">Lọc theo vị trí.</param>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CropDto>>> GetAll(
        [FromQuery] int? cropTypeId = null,
        [FromQuery] int? cropStatusId = null,
        [FromQuery] int? locationId = null)
    {
        var query = new GetAllCropsQuery(cropTypeId, cropStatusId, locationId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Lấy cây trồng theo ID.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CropDto>> GetById(int id)
    {
        var query = new GetCropByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound(new { message = "Cây trồng không tồn tại" });
        }

        return Ok(result);
    }

    /// <summary>
    /// Tạo mới cây trồng.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCropDto dto)
    {
        var command = new CreateCropCommand(
            dto.Name,
            dto.CropTypeId,
            dto.LocationId,
            dto.CropStatusId,
            dto.PlantDate,
            dto.ExpectedHarvestDate,
            dto.EstimatedYield,
            dto.Unit,
            dto.Note
        );

        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = newId }, new { id = newId });
    }

    /// <summary>
    /// Cập nhật cây trồng.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCropDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(new { message = "ID không khớp" });
        }

        var command = new UpdateCropCommand(
            id,
            dto.Name,
            dto.CropTypeId,
            dto.LocationId,
            dto.CropStatusId,
            dto.PlantDate,
            dto.ExpectedHarvestDate,
            dto.ActualHarvestDate,
            dto.EstimatedYield,
            dto.Unit,
            dto.Note
        );

        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Xóa cây trồng.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteCropCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}