using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FarmManagement.Application.DTOs.Reports.CropCost;
using FarmManagement.Application.Interfaces.Reports;

namespace FarmManagement.Infrastructure.Services.Reports;

public class CropCostReportService : ICropCostReportService
{
    private readonly ICropCostReportRepository _repository;

    public CropCostReportService(ICropCostReportRepository repository)
    {
        _repository = repository;
    }

    public async Task<CropCostReportResultDto> GetReportAsync(CropCostReportFilterDto filter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));
        if (filter.FromDate > filter.ToDate) throw new ArgumentException("FromDate must be less than or equal ToDate");

        // Normalize dates
        filter.FromDate = filter.FromDate.Date;
        filter.ToDate = filter.ToDate.Date;

        // Orchestrate calls to repository (data access lives only in repository)
        var summary = await _repository.GetSummaryAsync(filter).ConfigureAwait(false);
        var overall = await _repository.GetOverallTotalsAsync(filter).ConfigureAwait(false);

        // Compose result: include summary rows and overall aggregates
        return new CropCostReportResultDto
        {
            FilterUsed = filter,
            SummaryRows = summary.Rows,
            DetailRows = Array.Empty<CropCostDetailDto>(),
            TotalAmount = overall.TotalAmount,
            TotalQuantity = overall.TotalQuantity,
            TransactionCount = overall.TransactionCount
        };
    }

    public Task<CropCostSummaryPagedResultDto> GetSummaryAsync(CropCostReportFilterDto filter, CancellationToken cancellationToken = default)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));
        // Basic validation
        if (filter.FromDate > filter.ToDate) throw new ArgumentException("FromDate must be <= ToDate");

        return _repository.GetSummaryAsync(filter, cancellationToken);
    }

    public Task<CropCostDetailPagedResultDto> GetDetailsAsync(CropCostReportFilterDto filter, string? timePeriodKey = null, int? costTypeId = null, int? cropId = null, int? locationId = null, int? cropTypeId = null, CancellationToken cancellationToken = default)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));
        if (filter.FromDate > filter.ToDate) throw new ArgumentException("FromDate must be <= ToDate");

        return _repository.GetDetailsAsync(filter, timePeriodKey, costTypeId, cropId, locationId, cropTypeId, cancellationToken);
    }

    public async Task<Stream> ExportAsync(CropCostReportFilterDto filter, CancellationToken cancellationToken = default)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));
        if (filter.FromDate > filter.ToDate) throw new ArgumentException("FromDate must be <= ToDate");

        // Repository returns rows for export
        var rows = await _repository.GetExportRowsAsync(filter, cancellationToken).ConfigureAwait(false);

        // Build CSV in memory (service coordinates export but doesn't query DB directly)
        var ms = new MemoryStream();
        using (var sw = new StreamWriter(ms, System.Text.Encoding.UTF8, 1024, leaveOpen: true))
        {
            await sw.WriteLineAsync("Id,CostDate,Crop,CostType,Location,Quantity,Unit,UnitPrice,TotalAmount,Note").ConfigureAwait(false);
            foreach (var r in rows)
            {
                var note = r.Note?.Replace("\"", "\"\"") ?? string.Empty;
                var line = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0},{1:yyyy-MM-dd},{2},{3},{4},{5},{6},{7},{8},\"{9}\"",
                    r.CropCostId,
                    r.CostDate,
                    EscapeCsv(r.CropName),
                    EscapeCsv(r.CostTypeName),
                    EscapeCsv(r.LocationName),
                    r.Quantity,
                    EscapeCsv(r.Unit),
                    r.UnitPrice,
                    r.TotalAmount,
                    note);

                await sw.WriteLineAsync(line).ConfigureAwait(false);
            }
            await sw.FlushAsync().ConfigureAwait(false);
        }

        ms.Position = 0;
        return ms;
    }

    private static string EscapeCsv(string? input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        var needsQuotes = input.Contains(',') || input.Contains('"') || input.Contains('\n');
        var escaped = input.Replace("\"", "\"\"");
        return needsQuotes ? $"\"{escaped}\"" : escaped;
    }
}
