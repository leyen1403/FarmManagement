using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Features.LivestockSales.Commands.CreateLivestockSale;
using FarmManagement.Application.Features.LivestockSales.Commands.DeleteLivestockSale;
using FarmManagement.Application.Features.LivestockSales.Commands.UpdateLivestockSale;
using FarmManagement.Application.Features.LivestockSales.Queries.GetLivestockSaleById;
using FarmManagement.Application.Features.LivestockSales.Queries.GetLivestockSalesByLivestockId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Livestocks;

/// <summary>
/// Controller quản lý giao dịch bán vật nuôi (LivestockSale).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LivestockSalesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LivestockSalesController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Lấy tất cả giao dịch bán của một vật nuôi.
    /// </summary>
    /// <param name="livestockId">ID của vật nuôi.</param>
    [HttpGet("livestock/{livestockId:int}")]
    public async Task<IActionResult> GetByLivestockId(int livestockId)
    {
        var query = new GetLivestockSalesByLivestockIdQuery(livestockId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Lấy giao dịch bán theo ID.
    /// </summary>
    /// <param name="id">ID của giao dịch bán.</param>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetLivestockSaleByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = "Giao dịch bán không tồn tại." });

        return Ok(result);
    }

    /// <summary>
    /// Tạo mới giao dịch bán vật nuôi.
    /// </summary>
    /// <param name="dto">Thông tin giao dịch bán cần tạo.</param>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLivestockSaleDto dto)
    {
        var command = new CreateLivestockSaleCommand(dto);
        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = newId }, null);
    }

    /// <summary>
    /// Cập nhật giao dịch bán vật nuôi.
    /// </summary>
    /// <param name="id">ID của giao dịch bán.</param>
    /// <param name="dto">Thông tin cập nhật.</param>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateLivestockSaleDto dto)
    {
        dto.Id = id;
        var command = new UpdateLivestockSaleCommand(id, dto);
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Xóa giao dịch bán vật nuôi.
    /// </summary>
    /// <param name="id">ID của giao dịch bán cần xóa.</param>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteLivestockSaleCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
