using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Application.Exceptions;
using FarmManagement.Application.Features.LocationTypes.Commands.CreateLocationType;
using FarmManagement.Application.Features.LocationTypes.Commands.DeleteLocationType;
using FarmManagement.Application.Features.LocationTypes.Commands.UpdateLocationType;
using FarmManagement.Application.Features.LocationTypes.Queries.GetAllLocationTypes;
using FarmManagement.Application.Features.LocationTypes.Queries.GetLocationTypeById;
using FarmManagement.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Locations;

/// <summary>
/// Controller quản lý loại Location (LocationType).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LocationTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Khởi tạo LocationTypesController với IMediator.
    /// </summary>
    /// <param name="mediator">MediatR mediator.</param>
    public LocationTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy tất cả loại Location.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllLocationTypesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Lấy loại Location theo ID.
    /// </summary>
    /// <param name="id">ID của loại Location.</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetLocationTypeByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Tạo mới loại Location.
    /// </summary>
    /// <param name="dto">Thông tin loại Location cần tạo.</param>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LocationTypeDto dto)
    {
        try
        {
            var command = new CreateLocationTypeCommand(dto.Code, dto.Name);
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
    /// Cập nhật loại Location.
    /// </summary>
    /// <param name="id">ID của loại Location.</param>
    /// <param name="dto">Thông tin cập nhật.</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] LocationTypeDto dto)
    {
        try
        {
            var command = new UpdateLocationTypeCommand(id, dto.Code, dto.Name);
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
    /// Xóa loại Location.
    /// </summary>
    /// <param name="id">ID của loại Location cần xóa.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var command = new DeleteLocationTypeCommand(id);
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