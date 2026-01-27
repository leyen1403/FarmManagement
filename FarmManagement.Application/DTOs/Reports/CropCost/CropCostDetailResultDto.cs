using System;
using System.Collections.Generic;

namespace FarmManagement.Application.DTOs.Reports.CropCost;

/// <summary>
/// Paged result for drill-down (detail) view of a summary group.
/// Contains keys that link back to the summary row and paged detail rows.
/// No logic, only data.
/// </summary>
public class CropCostDetailPagedResultDto
{
    // --- Link keys to identify the originating summary group ---

    /// <summary>
    /// Time period key matching summary (e.g. "2026-01", "2026-Q1", or "2026-01-15").
    /// </summary>
    public string? TimePeriodKey { get; set; }

    public int? TimePeriodYear { get; set; }
    public int? TimePeriodMonth { get; set; }

    /// <summary>
    /// Dimension keys that may have been used in the summary grouping.
    /// These allow UI to correlate the details with the summary row.
    /// </summary>
    public int? CostTypeId { get; set; }
    public int? CropId { get; set; }
    public int? LocationId { get; set; }
    public int? CropTypeId { get; set; }

    // Optional display labels copied from summary to help UI
    public string? CostTypeName { get; set; }
    public string? CropName { get; set; }
    public string? LocationName { get; set; }
    public string? CropTypeName { get; set; }

    // --- Paging metadata ---

    /// <summary>
    /// Current page (1-based).
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Page size requested.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total number of matching detail rows for this group.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// True if there are more pages after the current one.
    /// </summary>
    public bool HasMore { get; set; }

    // --- Aggregates for the group (optional, useful for footer) ---
    public decimal TotalAmount { get; set; }
    public decimal TotalQuantity { get; set; }
    public int TransactionCount { get; set; }

    // --- Detail rows for the current page ---
    public IEnumerable<CropCostDetailDto> Rows { get; set; } = Array.Empty<CropCostDetailDto>();
}
