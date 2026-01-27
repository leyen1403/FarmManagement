using System;

namespace FarmManagement.Application.DTOs.Reports.CropCost;

/// <summary>
/// Request DTO for drill-down details API. Contains filter context and group keys.
/// No logic.
/// </summary>
public class CropCostReportDetailRequestDto
{
    public CropCostReportFilterDto Filter { get; set; } = new CropCostReportFilterDto();

    public string? TimePeriodKey { get; set; }

    public int? CostTypeId { get; set; }

    public int? CropId { get; set; }

    public int? LocationId { get; set; }

    public int? CropTypeId { get; set; }
}
