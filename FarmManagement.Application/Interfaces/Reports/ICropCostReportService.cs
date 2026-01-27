using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FarmManagement.Application.DTOs.Reports.CropCost;

namespace FarmManagement.Application.Interfaces.Reports;

public interface ICropCostReportService
{
    /// <summary>
    /// Generate full report result (legacy method).
    /// </summary>
    Task<CropCostReportResultDto> GetReportAsync(CropCostReportFilterDto filter);

    /// <summary>
    /// Get paged summary rows for the report according to the filter.
    /// </summary>
    Task<CropCostSummaryPagedResultDto> GetSummaryAsync(CropCostReportFilterDto filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get paged detail (drill-down) rows for a specific summary group.
    /// Pass group keys (timePeriodKey and/or dimension ids) alongside the filter.
    /// </summary>
    Task<CropCostDetailPagedResultDto> GetDetailsAsync(
        CropCostReportFilterDto filter,
        string? timePeriodKey = null,
        int? costTypeId = null,
        int? cropId = null,
        int? locationId = null,
        int? cropTypeId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Export the report according to the provided filter. Returns a stream for the generated file.
    /// Implementation may stream the file or enqueue a background job as needed.
    /// </summary>
    Task<Stream> ExportAsync(CropCostReportFilterDto filter, CancellationToken cancellationToken = default);
}
