using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Features.SaleTypes.Commands.CreateSaleType;
using FarmManagement.Application.Features.SaleTypes.Commands.DeleteSaleType;
using FarmManagement.Application.Features.SaleTypes.Commands.UpdateSaleType;
using FarmManagement.Application.Features.SaleTypes.Queries.GetAllSaleTypes;
using FarmManagement.Application.Features.SaleTypes.Queries.GetSaleTypeById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Livestocks;

/// <summary>
/// Controller quản lý loại bán vật nuôi (SaleType).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SaleTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SaleTypesController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Lấy tất cả loại bán vật nuôi.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllSaleTypesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Lấy loại bán vật nuôi theo ID.
    /// </summary>
    /// <param name="id">ID của loại bán.</param>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetSaleTypeByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = "Loại bán vật nuôi không tồn tại." });

        return Ok(result);
    }

    /// <summary>
    /// Tạo mới loại bán vật nuôi.
    /// </summary>
    /// <param name="dto">Thông tin loại bán cần tạo.</param>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSaleTypeDto dto)
    {
        var command = new CreateSaleTypeCommand(dto);
        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = newId }, null);
    }

    /// <summary>
    /// Cập nhật loại bán vật nuôi.
    /// </summary>
    /// <param name="id">ID của loại bán.</param>
    /// <param name="dto">Thông tin cập nhật.</param>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateSaleTypeDto dto)
    {
        dto.Id = id;
        var command = new UpdateSaleTypeCommand(id, dto);
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Xóa loại bán vật nuôi.
    /// </summary>
    /// <param name="id">ID của loại bán cần xóa.</param>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteSaleTypeCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
