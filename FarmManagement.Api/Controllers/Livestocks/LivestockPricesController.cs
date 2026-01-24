using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Livestocks;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Livestocks;

[ApiController]
[Route("api/[controller]")]
public class LivestockPricesController : ControllerBase
{
    private readonly ILivestockPriceService _priceService;

    public LivestockPricesController(ILivestockPriceService priceService)
    {
        _priceService = priceService;
    }

    [HttpGet("livestock/{livestockId:int}")]
    public async Task<IActionResult> GetByLivestockId(int livestockId)
    {
        var items = await _priceService.GetByLivestockIdAsync(livestockId);
        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var dto = await _priceService.GetByIdAsync(id);
        if (dto == null)
            return NotFound();
        return Ok(dto);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive([FromQuery] int livestockId, [FromQuery] int gender, [FromQuery] DateTime? at)
    {
        var date = at ?? DateTime.Today;
        var dto = await _priceService.GetActivePriceAsync(livestockId, (FarmManagement.Domain.Entities.Livestocks.GenderType)gender, date);
        if (dto == null)
            return NotFound();
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LivestockPriceDto dto)
    {
        var id = await _priceService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = id }, null);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] LivestockPriceDto dto)
    {
        await _priceService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _priceService.DeleteAsync(id);
        return NoContent();
    }
}
