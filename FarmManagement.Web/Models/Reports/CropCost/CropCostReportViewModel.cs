using System.Collections.Generic;
using FarmManagement.Application.DTOs.Reports.CropCost;

namespace FarmManagement.Web.Models.Reports.CropCost;

/// <summary>
/// ViewModel for Crop Cost report page.
/// Contains only data for rendering UI: filter, summary paging result and lookup lists.
/// No logic here.
/// </summary>
public class CropCostReportViewModel
{
    /// <summary>
    /// Filter bound from query string / form.
    /// </summary>
    public CropCostReportFilterDto Filter { get; set; } = new CropCostReportFilterDto();

    /// <summary>
    /// Paged summary result for current filter/page.
    /// </summary>
    public CropCostSummaryPagedResultDto? SummaryPagedResult { get; set; }

    /// <summary>
    /// Optional details for drill-down (current page of details).
    /// </summary>
    public CropCostDetailPagedResultDto? DetailPagedResult { get; set; }

    /// <summary>
    /// Lookup items for CostType dropdown / multi-select.
    /// </summary>
    public IEnumerable<LookupItemDto> CostTypes { get; set; } = System.Array.Empty<LookupItemDto>();

    /// <summary>
    /// Lookup items for Crop dropdown / multi-select.
    /// </summary>
    public IEnumerable<LookupItemDto> Crops { get; set; } = System.Array.Empty<LookupItemDto>();

    /// <summary>
    /// Lookup items for Location dropdown / multi-select.
    /// </summary>
    public IEnumerable<LookupItemDto> Locations { get; set; } = System.Array.Empty<LookupItemDto>();

    /// <summary>
    /// Lookup items for CropType dropdown / multi-select.
    /// </summary>
    public IEnumerable<LookupItemDto> CropTypes { get; set; } = System.Array.Empty<LookupItemDto>();
}

/// <summary>
/// Simple lookup item used by UI dropdowns.
/// </summary>
public class LookupItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}