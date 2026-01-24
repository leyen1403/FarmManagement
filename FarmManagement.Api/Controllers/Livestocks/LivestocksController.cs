using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Features.Livestocks.Commands.CreateLivestock;
using FarmManagement.Application.Features.Livestocks.Commands.DeleteLivestock;
using FarmManagement.Application.Features.Livestocks.Commands.UpdateLivestock;
using FarmManagement.Application.Features.Livestocks.Queries.GetAllLivestocks;
using FarmManagement.Application.Features.Livestocks.Queries.GetLivestockById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Livestocks;

[ApiController]
[Route("api/[controller]")]
public class LivestocksController : ControllerBase
{
    private readonly IMediator _mediator;
    public LivestocksController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? livestockTypeId = null, [FromQuery] int? livestockStatusId = null, [FromQuery] int? locationId = null)
    {
        var query = new GetAllLivestocksQuery(livestockTypeId, livestockStatusId, locationId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetLivestockByIdQuery(id);
        var result = await _mediator.Send(query);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLivestockDto dto)
    {
        var command = new CreateLivestockCommand(dto);
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateLivestockDto dto)
    {
        var command = new UpdateLivestockCommand(id, dto);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteLivestockCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
