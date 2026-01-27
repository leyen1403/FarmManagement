using FarmManagement.Application.DTOs.Reports.CropCost;
using FarmManagement.Application.Interfaces.Reports;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Reports;

[ApiController]
[Route("api/reports/cropcost")]
public class CropCostController : ControllerBase
{
    private readonly ICropCostReportService _reportService;

    public CropCostController(ICropCostReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("summary")]
    public async Task<ActionResult<CropCostSummaryPagedResultDto>> GetSummary([FromQuery] CropCostReportFilterDto filter, CancellationToken cancellationToken)
    {
        if (filter == null) return BadRequest();
        var result = await _reportService.GetSummaryAsync(filter, cancellationToken);
        return Ok(result);
    }

    [HttpGet("details")]
    public async Task<ActionResult<CropCostDetailPagedResultDto>> GetDetails([FromQuery] CropCostReportFilterDto filter, [FromQuery] string? timePeriodKey, [FromQuery] int? costTypeId, [FromQuery] int? cropId, [FromQuery] int? locationId, [FromQuery] int? cropTypeId, CancellationToken cancellationToken)
    {
        if (filter == null) return BadRequest();
        var result = await _reportService.GetDetailsAsync(filter, timePeriodKey, costTypeId, cropId, locationId, cropTypeId, cancellationToken);
        return Ok(result);
    }

    [HttpPost("export")]
    public async Task<IActionResult> Export([FromBody] CropCostReportFilterDto filter, CancellationToken cancellationToken)
    {
        if (filter == null) return BadRequest();

        var stream = await _reportService.ExportAsync(filter, cancellationToken);
        if (stream == null) return StatusCode(500);

        if (stream.CanSeek) stream.Position = 0;
        string fileName = $"crop-cost-report-{DateTime.UtcNow:yyyyMMddHHmmss}.csv";
        return File(stream, "text/csv", fileName);
    }
}
