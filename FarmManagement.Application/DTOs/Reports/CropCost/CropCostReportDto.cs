using System;
using System.Collections.Generic;

namespace FarmManagement.Application.DTOs.Reports.CropCost;

public class CropCostSummaryDto
{
    public string TimePeriodKey { get; set; } = string.Empty;
    public int? TimePeriodYear { get; set; }
    public int? TimePeriodMonth { get; set; }

    public int? CostTypeId { get; set; }
    public string? CostTypeName { get; set; }

    public int? CropId { get; set; }
    public string? CropName { get; set; }

    public int? LocationId { get; set; }
    public string? LocationName { get; set; }

    public int? CropTypeId { get; set; }
    public string? CropTypeName { get; set; }

    public decimal TotalAmount { get; set; }
    public decimal TotalQuantity { get; set; }
    public int TransactionCount { get; set; }
    public decimal AvgUnitPrice { get; set; }
}

public class CropCostDetailDto
{
    public int CropCostId { get; set; }
    public DateTime CostDate { get; set; }
    public int CropId { get; set; }
    public string? CropName { get; set; }
    public int CostTypeId { get; set; }
    public string? CostTypeName { get; set; }
    public int LocationId { get; set; }
    public string? LocationName { get; set; }
    public decimal Quantity { get; set; }
    public string? Unit { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Note { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class CropCostReportResultDto
{
    public CropCostReportFilterDto FilterUsed { get; set; } = new CropCostReportFilterDto();
    public IEnumerable<CropCostSummaryDto> SummaryRows { get; set; } = Array.Empty<CropCostSummaryDto>();
    public IEnumerable<CropCostDetailDto> DetailRows { get; set; } = Array.Empty<CropCostDetailDto>();
    public decimal TotalAmount { get; set; }
    public decimal TotalQuantity { get; set; }
    public int TransactionCount { get; set; }
}

/// <summary>
/// Paged result DTO for summary table. Contains rows for the current page,
/// paging metadata and aggregates for the entire filtered dataset.
/// No navigation properties.
/// </summary>
public class CropCostSummaryPagedResultDto
{
    /// <summary>
    /// Rows for the current page.
    /// </summary>
    public IEnumerable<CropCostSummaryDto> Rows { get; set; } = Array.Empty<CropCostSummaryDto>();

    /// <summary>
    /// Current page index (1-based).
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Size of a page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total number of rows matching the filter (for all pages).
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Indicator whether there are more pages after current.
    /// </summary>
    public bool HasMore { get; set; }

    /// <summary>
    /// Aggregate totals for the entire filtered dataset (optional, helpful for footer/KPI).
    /// </summary>
    public decimal TotalAmount { get; set; }

    public decimal TotalQuantity { get; set; }
    public int TransactionCount { get; set; }
}
