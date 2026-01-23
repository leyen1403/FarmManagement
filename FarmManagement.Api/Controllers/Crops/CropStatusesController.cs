using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Application.Features.CropStatuses.Commands.CreateCropStatus;
using FarmManagement.Application.Features.CropStatuses.Commands.UpdateCropStatus;
using FarmManagement.Application.Features.CropStatuses.Commands.DeleteCropStatus;
using FarmManagement.Application.Features.CropStatuses.Queries.GetAllCropStatuses;
using FarmManagement.Application.Features.CropStatuses.Queries.GetCropStatusById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers;

/// <summary>
/// Controller quản lý trạng thái cây trồng (CropStatus).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CropStatusController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Khởi tạo CropStatusController với IMediator.
    /// </summary>
    /// <param name="mediator">MediatR mediator.</param>
    public CropStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy tất cả trạng thái cây trồng.
    /// </summary>
    /// <param name="includeInactive">Bao gồm trạng thái không hoạt động.</param>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CropStatusDto>>> GetAll([FromQuery] bool includeInactive = false)
    {
        try
        {
            var query = new GetAllCropStatusesQuery(includeInactive);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Success = false, Message = ex.Message });
        }
    }

    /// <summary>
    /// Lấy trạng thái cây trồng theo ID.
    /// </summary>
    /// <param name="id">ID của trạng thái cây trồng.</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<CropStatusDto>> GetById(int id)
    {
        try
        {
            var query = new GetCropStatusByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound(new { Success = false, Message = $"CropStatus {id} not found" });

            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { Success = false, Message = $"CropStatus {id} not found" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Success = false, Message = ex.Message });
        }
    }

    /// <summary>
    /// Tạo mới trạng thái cây trồng.
    /// </summary>
    /// <param name="dto">Thông tin trạng thái cây trồng cần tạo.</param>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCropStatusDto dto)
    {
        if (dto == null)
            return BadRequest(new { Success = false, Message = "Invalid data" });

        try
        {
            var command = new CreateCropStatusCommand(dto.Code, dto.Name, dto.Description);
            await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = dto.Code }, dto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Success = false, Message = ex.Message });
        }
    }

    /// <summary>
    /// Cập nhật trạng thái cây trồng.
    /// </summary>
    /// <param name="id">ID của trạng thái cây trồng.</param>
    /// <param name="dto">Thông tin cập nhật.</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCropStatusDto dto)
    {
        if (dto == null || id != dto.Id)
            return BadRequest(new { Success = false, Message = "Invalid data" });

        try
        {
            var command = new UpdateCropStatusCommand(id, dto.Code, dto.Name, dto.Description, dto.IsActive);
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { Success = false, Message = $"CropStatus {id} not found" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Success = false, Message = ex.Message });
        }
    }

    /// <summary>
    /// Xóa trạng thái cây trồng.
    /// </summary>
    /// <param name="id">ID của trạng thái cây trồng cần xóa.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var command = new DeleteCropStatusCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { Success = false, Message = $"CropStatus {id} not found" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Success = false, Message = ex.Message });
        }
    }
}
