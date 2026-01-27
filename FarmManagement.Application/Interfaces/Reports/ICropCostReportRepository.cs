using System.Threading;
using System.Threading.Tasks;
using FarmManagement.Application.DTOs.Reports.CropCost;

namespace FarmManagement.Application.Interfaces.Reports;

/// <summary>
/// Repository interface for reading CropCost report data.
/// Read-only, returns DTOs or projection records (no entities).
/// </summary>
public interface ICropCostReportRepository
{
    /// <summary>
    /// Query summary data for crop cost report by provided filter with paging/sorting handled by repository.
    /// Returns paged summary result DTO.
    /// </summary>
    Task<CropCostSummaryPagedResultDto> GetSummaryAsync(CropCostReportFilterDto filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Query detail (drill-down) data for a given group key and filter. Repository handles paging.
    /// </summary>
    Task<CropCostDetailPagedResultDto> GetDetailsAsync(CropCostReportFilterDto filter, string? timePeriodKey = null, int? costTypeId = null, int? cropId = null, int? locationId = null, int? cropTypeId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Compute overall totals for a given filter (TotalAmount, TotalQuantity, TransactionCount)
    /// </summary>
    Task<(decimal TotalAmount, decimal TotalQuantity, int TransactionCount)> GetOverallTotalsAsync(CropCostReportFilterDto filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Query rows required for export. Repository returns projection rows suitable for export.
    /// </summary>
    Task<IEnumerable<CropCostDetailDto>> GetExportRowsAsync(CropCostReportFilterDto filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Legacy methods (kept for compatibility) - can be removed after migration
    /// </summary>
    Task<IEnumerable<CropCostSummaryDto>> QuerySummaryAsync(CropCostReportFilterDto filter);
    Task<IEnumerable<CropCostDetailDto>> QueryDetailsAsync(CropCostReportFilterDto filter);
}
