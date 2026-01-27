using System;

namespace FarmManagement.Application.DTOs.Reports.CropCost;

/// <summary>
/// Time grain for report aggregation.
/// </summary>
public enum TimeGrain
{
    Day,
    Month,
    Quarter,
    Year
}

/// <summary>
/// Fields available to group by in the report.
/// </summary>
public enum CropCostGroupBy
{
    TimePeriod,
    CostType,
    Crop,
    Location,
    CropType
}

/// <summary>
/// Fields available to sort the report results.
/// </summary>
public enum CropCostSortBy
{
    TimePeriod,
    TotalAmount,
    TotalQuantity,
    TransactionCount,
    AvgUnitPrice
}

/// <summary>
/// Sort direction.
/// </summary>
public enum SortDirection
{
    Asc,
    Desc
}

/// <summary>
/// Filter and options for Crop Cost report.
/// Contains only data (no logic).
/// </summary>
public class CropCostReportFilterDto
{
    // Date range (inclusive)
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    // Aggregation time grain
    public TimeGrain TimeGrain { get; set; } = TimeGrain.Month;

    // Which dimensions to group by
    public CropCostGroupBy[]? GroupBy { get; set; }

    // Filters (multi-select)
    public int[]? CostTypeIds { get; set; }

    public int[]? CropIds { get; set; }

    public int[]? LocationIds { get; set; }

    public int[]? CropTypeIds { get; set; }

    // Include inactive records (if applicable)
    public bool IncludeInactive { get; set; } = false;

    // Numeric filters
    public decimal? MinTotalAmount { get; set; }

    public decimal? MaxTotalAmount { get; set; }

    // Top N results (optional)
    public int? TopN { get; set; }

    // Paging
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 50;

    // Sorting
    public CropCostSortBy? SortBy { get; set; }

    public SortDirection SortDirection { get; set; } = SortDirection.Desc;
}
