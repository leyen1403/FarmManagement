using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Application.Exceptions;
using FarmManagement.Application.Features.LocationStatuses.Commands.CreateLocationStatus;
using FarmManagement.Application.Features.LocationStatuses.Commands.DeleteLocationStatus;
using FarmManagement.Application.Features.LocationStatuses.Commands.UpdateLocationStatus;
using FarmManagement.Application.Features.LocationStatuses.Queries.GetAllLocationStatuses;
using FarmManagement.Application.Features.LocationStatuses.Queries.GetLocationStatusById;
using FarmManagement.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Locations;

/// <summary>
/// Controller quản lý trạng thái Location (LocationStatus).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LocationStatusesController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Khởi tạo LocationStatusesController với IMediator.
    /// </summary>
    /// <param name="mediator">MediatR mediator.</param>
    public LocationStatusesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy tất cả trạng thái Location.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllLocationStatusesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Lấy trạng thái Location theo ID.
    /// </summary>
    /// <param name="id">ID của trạng thái Location.</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetLocationStatusByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Tạo mới trạng thái Location.
    /// </summary>
    /// <param name="dto">Thông tin trạng thái Location cần tạo.</param>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LocationStatusDto dto)
    {
        try
        {
            var command = new CreateLocationStatusCommand(dto.Code, dto.Name);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (BusinessException ex)
        {
            return BadRequest(new ApiErrorResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Cập nhật trạng thái Location.
    /// </summary>
    /// <param name="id">ID của trạng thái Location.</param>
    /// <param name="dto">Thông tin cập nhật.</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] LocationStatusDto dto)
    {
        try
        {
            var command = new UpdateLocationStatusCommand(id, dto.Code, dto.Name);
            await _mediator.Send(command);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new ApiErrorResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (BusinessException ex)
        {
            return BadRequest(new ApiErrorResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Xóa trạng thái Location.
    /// </summary>
    /// <param name="id">ID của trạng thái Location cần xóa.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var command = new DeleteLocationStatusCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
        catch (BusinessException ex)
        {
            return BadRequest(new ApiErrorResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
    }
}