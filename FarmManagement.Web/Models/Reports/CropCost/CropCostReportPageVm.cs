using System;
using System.Collections.Generic;
using FarmManagement.Application.DTOs.Reports.CropCost;

namespace FarmManagement.Web.Models.Reports.CropCost;

/// <summary>
/// Page ViewModel for Crop Cost report UI. Contains only data for rendering the page.
/// Does not expose or reuse application DTOs directly.
/// No logic here.
/// </summary>
public class CropCostReportPageVm
{
    public CropCostReportPageVm()
    {
        Filter = new CropCostReportFilterVm();
        SummaryRows = new List<CropCostSummaryRowVm>();
        OverallTotals = new CropCostOverallTotalsVm();
        Paging = new PagingInfoVm();
        CostTypes = new List<LookupItemVm>();
        Crops = new List<LookupItemVm>();
        Locations = new List<LookupItemVm>();
    }

    public CropCostReportFilterVm Filter { get; set; }

    // Legacy mapped rows for UI convenience
    public IEnumerable<CropCostSummaryRowVm> SummaryRows { get; set; }

    // Expose raw summary DTO so view can access Rows directly if desired
    public CropCostSummaryPagedResultDto? Summary { get; set; }

    public CropCostOverallTotalsVm OverallTotals { get; set; }

    public PagingInfoVm Paging { get; set; }

    // Detail paged result for drill-down (kept as DTO for simplicity in UI rendering)
    public CropCostDetailPagedResultDto? DetailPagedResult { get; set; }

    // Lookup lists for filter selects
    public IEnumerable<LookupItemVm> CostTypes { get; set; }
    public IEnumerable<LookupItemVm> Crops { get; set; }
    public IEnumerable<LookupItemVm> Locations { get; set; }
}

public class LookupItemVm
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Simple filter view model for UI binding/display. Keep fields needed by the page only.
/// </summary>
public class CropCostReportFilterVm
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }

    // Time grain as string for display/bind; controllers can map between enum and string.
    public string? TimeGrain { get; set; }

    // Multi-select ids (UI uses these to render selected options)
    public int[]? CostTypeIds { get; set; }
    public int[]? CropIds { get; set; }
    public int[]? LocationIds { get; set; }
    public int[]? CropTypeIds { get; set; }

    public bool IncludeInactive { get; set; }

    public int Page { get; set; }
    public int PageSize { get; set; }
}

public class CropCostSummaryRowVm
{
    public string TimePeriodKey { get; set; } = string.Empty;
    public int? CostTypeId { get; set; }
    public int? CropId { get; set; }
    public int? LocationId { get; set; }
    public int? CropTypeId { get; set; }

    public string? CostTypeName { get; set; }
    public string? CropName { get; set; }
    public string? LocationName { get; set; }

    public decimal TotalAmount { get; set; }
    public decimal TotalQuantity { get; set; }
    public int TransactionCount { get; set; }
}

public class CropCostOverallTotalsVm
{
    public decimal TotalAmount { get; set; }
    public decimal TotalQuantity { get; set; }
    public int TransactionCount { get; set; }
}

public class PagingInfoVm
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasMore { get; set; }
}
