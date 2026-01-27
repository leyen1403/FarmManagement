using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FarmManagement.Application.DTOs.Reports.CropCost;
using FarmManagement.Application.Interfaces.Reports;
using FarmManagement.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.Infrastructure.Repositories.Reports;

public class CropCostReportRepository : ICropCostReportRepository
{
    private readonly FarmManagementDbContext _context;

    public CropCostReportRepository(FarmManagementDbContext context)
    {
        _context = context;
    }

    // Legacy methods (map to new ones)
    public async Task<IEnumerable<CropCostSummaryDto>> QuerySummaryAsync(CropCostReportFilterDto filter)
    {
        var paged = await GetSummaryAsync(filter).ConfigureAwait(false);
        return paged.Rows;
    }

    public async Task<IEnumerable<CropCostDetailDto>> QueryDetailsAsync(CropCostReportFilterDto filter)
    {
        var details = await GetDetailsAsync(filter).ConfigureAwait(false);
        return details.Rows;
    }

    public async Task<CropCostSummaryPagedResultDto> GetSummaryAsync(CropCostReportFilterDto filter, CancellationToken cancellationToken = default)
    {
        // Validate filter
        if (filter == null) throw new ArgumentNullException(nameof(filter));
        if (filter.FromDate > filter.ToDate) throw new ArgumentException("FromDate must be <= ToDate");

        DateTime from = filter.FromDate.Date;
        DateTime toExclusive = filter.ToDate.Date.AddDays(1);

        var baseQuery = _context.CropCosts
            .AsNoTracking()
            .Where(x => x.CostDate >= from && x.CostDate < toExclusive);

        if (filter.CostTypeIds != null && filter.CostTypeIds.Length > 0)
            baseQuery = baseQuery.Where(x => filter.CostTypeIds.Contains(x.CostTypeId));

        if (filter.CropIds != null && filter.CropIds.Length > 0)
            baseQuery = baseQuery.Where(x => filter.CropIds.Contains(x.CropId));

        if (filter.LocationIds != null && filter.LocationIds.Length > 0)
            baseQuery = baseQuery.Where(x => filter.LocationIds.Contains(x.Crop.LocationId));

        if (filter.CropTypeIds != null && filter.CropTypeIds.Length > 0)
            baseQuery = baseQuery.Where(x => filter.CropTypeIds.Contains(x.Crop.CropTypeId));

        // overall totals
        var overallAgg = await baseQuery
            .GroupBy(x => 1)
            .Select(g => new
            {
                TotalAmount = g.Sum(i => i.TotalAmount),
                TotalQuantity = g.Sum(i => i.Quantity),
                TransactionCount = g.Count()
            })
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        decimal overallTotalAmount = overallAgg?.TotalAmount ?? 0m;
        decimal overallTotalQuantity = overallAgg?.TotalQuantity ?? 0m;
        int overallTransactionCount = overallAgg?.TransactionCount ?? 0;

        // projection for grouping
        var projected = baseQuery.Select(x => new
        {
            Year = x.CostDate.Year,
            Month = x.CostDate.Month,
            Day = x.CostDate.Day,
            CropId = x.CropId,
            CropName = x.Crop.Name,
            CostTypeId = x.CostTypeId,
            CostTypeName = x.CostType.Name,
            LocationId = x.Crop.LocationId,
            LocationName = x.Crop.Location.Name,
            CropTypeId = x.Crop.CropTypeId,
            CropTypeName = x.Crop.CropType.Name,
            x.Quantity,
            x.UnitPrice,
            x.TotalAmount
        });

        var groupBySet = new HashSet<CropCostGroupBy>(filter.GroupBy ?? new[] { CropCostGroupBy.TimePeriod });
        bool includeCostType = groupBySet.Contains(CropCostGroupBy.CostType);
        bool includeCrop = groupBySet.Contains(CropCostGroupBy.Crop);
        bool includeLocation = groupBySet.Contains(CropCostGroupBy.Location);
        bool includeCropType = groupBySet.Contains(CropCostGroupBy.CropType);

        TimeGrain timeGrain = filter.TimeGrain;

        var grouped = projected.GroupBy(p => new
        {
            Year = p.Year,
            Month = (timeGrain == TimeGrain.Day || timeGrain == TimeGrain.Month || timeGrain == TimeGrain.Quarter) ? p.Month : (int?)null,
            Day = (timeGrain == TimeGrain.Day) ? p.Day : (int?)null,
            Quarter = (timeGrain == TimeGrain.Quarter) ? ((p.Month - 1) / 3) + 1 : (int?)null,
            CostTypeKey = includeCostType ? p.CostTypeId : (int?)null,
            CropKey = includeCrop ? p.CropId : (int?)null,
            LocationKey = includeLocation ? p.LocationId : (int?)null,
            CropTypeKey = includeCropType ? p.CropTypeId : (int?)null,
            CostTypeName = includeCostType ? p.CostTypeName : null,
            CropName = includeCrop ? p.CropName : null,
            LocationName = includeLocation ? p.LocationName : null,
            CropTypeName = includeCropType ? p.CropTypeName : null
        })
        .Select(g => new
        {
            Key = g.Key,
            TotalAmount = g.Sum(x => x.TotalAmount),
            TotalQuantity = g.Sum(x => x.Quantity),
            TransactionCount = g.Count(),
            AvgUnitPrice = g.Sum(y => y.TotalAmount) / (g.Sum(y => y.Quantity) == 0 ? 1 : g.Sum(y => y.Quantity))
        });

        // ordering
        bool asc = filter.SortDirection == SortDirection.Asc;
        IQueryable<dynamic> ordered;
        if (filter.SortBy.HasValue)
        {
            switch (filter.SortBy.Value)
            {
                case CropCostSortBy.TotalAmount:
                    ordered = asc ? grouped.OrderBy(x => x.TotalAmount) : grouped.OrderByDescending(x => x.TotalAmount);
                    break;
                case CropCostSortBy.TotalQuantity:
                    ordered = asc ? grouped.OrderBy(x => x.TotalQuantity) : grouped.OrderByDescending(x => x.TotalQuantity);
                    break;
                case CropCostSortBy.TransactionCount:
                    ordered = asc ? grouped.OrderBy(x => x.TransactionCount) : grouped.OrderByDescending(x => x.TransactionCount);
                    break;
                case CropCostSortBy.AvgUnitPrice:
                    ordered = asc ? grouped.OrderBy(x => x.AvgUnitPrice) : grouped.OrderByDescending(x => x.AvgUnitPrice);
                    break;
                case CropCostSortBy.TimePeriod:
                default:
                    ordered = asc ? grouped.OrderBy(x => x.Key.Year).ThenBy(x => x.Key.Month ?? 0).ThenBy(x => x.Key.Day ?? 0)
                                  : grouped.OrderByDescending(x => x.Key.Year).ThenByDescending(x => x.Key.Month ?? 0).ThenByDescending(x => x.Key.Day ?? 0);
                    break;
            }
        }
        else
        {
            ordered = grouped.OrderByDescending(x => x.TotalAmount);
        }

        int totalGroups = await ordered.CountAsync(cancellationToken).ConfigureAwait(false);

        IQueryable<dynamic> pagedQuery = ordered;
        if (filter.TopN.HasValue && filter.TopN.Value > 0)
            pagedQuery = pagedQuery.Take(filter.TopN.Value);
        else
        {
            int skip = (Math.Max(filter.Page, 1) - 1) * Math.Max(filter.PageSize, 1);
            pagedQuery = pagedQuery.Skip(skip).Take(filter.PageSize);
        }

        var list = await pagedQuery.ToListAsync(cancellationToken).ConfigureAwait(false);

        var rows = list.Select(item =>
        {
            string timePeriodKey;
            int? year = item.Key.Year;
            int? month = item.Key.Month;
            int? day = item.Key.Day;
            int? quarter = item.Key.Quarter;

            switch (timeGrain)
            {
                case TimeGrain.Day:
                    timePeriodKey = year.HasValue && month.HasValue && day.HasValue ? string.Format("{0:0000}-{1:00}-{2:00}", year.Value, month.Value, day.Value) : string.Empty;
                    break;
                case TimeGrain.Month:
                    timePeriodKey = year.HasValue && month.HasValue ? string.Format("{0:0000}-{1:00}", year.Value, month.Value) : string.Empty;
                    break;
                case TimeGrain.Quarter:
                    timePeriodKey = year.HasValue && quarter.HasValue ? string.Format("{0:0000}-Q{1}", year.Value, quarter.Value) : string.Empty;
                    break;
                case TimeGrain.Year:
                    timePeriodKey = year.HasValue ? string.Format("{0:0000}", year.Value) : string.Empty;
                    break;
                default:
                    timePeriodKey = string.Empty;
                    break;
            }

            return new CropCostSummaryDto
            {
                TimePeriodKey = timePeriodKey,
                TimePeriodYear = year,
                TimePeriodMonth = month,
                CostTypeId = item.Key.CostTypeKey,
                CostTypeName = item.Key.CostTypeName,
                CropId = item.Key.CropKey,
                CropName = item.Key.CropName,
                LocationId = item.Key.LocationKey,
                LocationName = item.Key.LocationName,
                CropTypeId = item.Key.CropTypeKey,
                CropTypeName = item.Key.CropTypeName,
                TotalAmount = item.TotalAmount,
                TotalQuantity = item.TotalQuantity,
                TransactionCount = item.TransactionCount,
                AvgUnitPrice = item.AvgUnitPrice
            };
        }).ToList();

        return new CropCostSummaryPagedResultDto
        {
            Rows = rows,
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalCount = totalGroups,
            HasMore = (filter.TopN.HasValue && filter.TopN.Value > 0) ? false : ((filter.Page * filter.PageSize) < totalGroups),
            TotalAmount = overallTotalAmount,
            TotalQuantity = overallTotalQuantity,
            TransactionCount = overallTransactionCount
        };
    }

    public async Task<CropCostDetailPagedResultDto> GetDetailsAsync(CropCostReportFilterDto filter, string? timePeriodKey = null, int? costTypeId = null, int? cropId = null, int? locationId = null, int? cropTypeId = null, CancellationToken cancellationToken = default)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));
        if (filter.FromDate > filter.ToDate) throw new ArgumentException("FromDate must be <= ToDate");

        DateTime from = filter.FromDate.Date;
        DateTime toExclusive = filter.ToDate.Date.AddDays(1);

        var query = _context.CropCosts
            .AsNoTracking()
            .Where(x => x.CostDate >= from && x.CostDate < toExclusive);

        if (filter.CostTypeIds != null && filter.CostTypeIds.Length > 0)
            query = query.Where(x => filter.CostTypeIds.Contains(x.CostTypeId));

        if (filter.CropIds != null && filter.CropIds.Length > 0)
            query = query.Where(x => filter.CropIds.Contains(x.CropId));

        if (filter.LocationIds != null && filter.LocationIds.Length > 0)
            query = query.Where(x => filter.LocationIds.Contains(x.Crop.LocationId));

        if (filter.CropTypeIds != null && filter.CropTypeIds.Length > 0)
            query = query.Where(x => filter.CropTypeIds.Contains(x.Crop.CropTypeId));

        if (!string.IsNullOrEmpty(timePeriodKey))
        {
            if (timePeriodKey.Contains("-Q"))
            {
                var parts = timePeriodKey.Split("-Q", StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2 && int.TryParse(parts[0], out int qYear) && int.TryParse(parts[1], out int qNum))
                {
                    int startMonth = (qNum - 1) * 3 + 1;
                    DateTime start = new DateTime(qYear, startMonth, 1);
                    DateTime end = start.AddMonths(3);
                    query = query.Where(x => x.CostDate >= start && x.CostDate < end);
                }
            }
            else if (DateTime.TryParseExact(timePeriodKey, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime d))
            {
                query = query.Where(x => x.CostDate.Date == d.Date);
            }
            else if (DateTime.TryParseExact(timePeriodKey, "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime m))
            {
                DateTime start = new DateTime(m.Year, m.Month, 1);
                DateTime end = start.AddMonths(1);
                query = query.Where(x => x.CostDate >= start && x.CostDate < end);
            }
            else if (DateTime.TryParseExact(timePeriodKey, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime y))
            {
                DateTime start = new DateTime(y.Year, 1, 1);
                DateTime end = start.AddYears(1);
                query = query.Where(x => x.CostDate >= start && x.CostDate < end);
            }
        }

        if (costTypeId.HasValue) query = query.Where(x => x.CostTypeId == costTypeId.Value);
        if (cropId.HasValue) query = query.Where(x => x.CropId == cropId.Value);
        if (locationId.HasValue) query = query.Where(x => x.Crop.LocationId == locationId.Value);
        if (cropTypeId.HasValue) query = query.Where(x => x.Crop.CropTypeId == cropTypeId.Value);

        var agg = await query
            .GroupBy(x => 1)
            .Select(g => new
            {
                TotalAmount = g.Sum(i => i.TotalAmount),
                TotalQuantity = g.Sum(i => i.Quantity),
                TotalCount = g.Count()
            })
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        int totalCount = agg?.TotalCount ?? 0;
        decimal totalAmount = agg?.TotalAmount ?? 0m;
        decimal totalQuantity = agg?.TotalQuantity ?? 0m;

        int page = Math.Max(filter.Page, 1);
        int pageSize = Math.Max(filter.PageSize, 1);
        int skip = (page - 1) * pageSize;

        var rows = await query
            .OrderByDescending(x => x.CostDate)
            .Skip(skip)
            .Take(pageSize)
            .Select(x => new CropCostDetailDto
            {
                CropCostId = x.Id,
                CostDate = x.CostDate,
                CropId = x.CropId,
                CropName = x.Crop.Name,
                CostTypeId = x.CostTypeId,
                CostTypeName = x.CostType.Name,
                LocationId = x.Crop.LocationId,
                LocationName = x.Crop.Location.Name,
                Quantity = x.Quantity,
                Unit = x.Unit,
                UnitPrice = x.UnitPrice,
                TotalAmount = x.TotalAmount,
                Note = x.Note,
                CreatedDate = x.CreatedDate
            })
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return new CropCostDetailPagedResultDto
        {
            TimePeriodKey = timePeriodKey,
            TimePeriodYear = null,
            TimePeriodMonth = null,
            CostTypeId = costTypeId,
            CropId = cropId,
            LocationId = locationId,
            CropTypeId = cropTypeId,
            CostTypeName = null,
            CropName = null,
            LocationName = null,
            CropTypeName = null,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            HasMore = (skip + rows.Count) < totalCount,
            TotalAmount = totalAmount,
            TotalQuantity = totalQuantity,
            TransactionCount = totalCount,
            Rows = rows
        };
    }

    public async Task<(decimal TotalAmount, decimal TotalQuantity, int TransactionCount)> GetOverallTotalsAsync(CropCostReportFilterDto filter, CancellationToken cancellationToken = default)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));
        if (filter.FromDate > filter.ToDate) throw new ArgumentException("FromDate must be <= ToDate");

        DateTime from = filter.FromDate.Date;
        DateTime toExclusive = filter.ToDate.Date.AddDays(1);

        var baseQuery = _context.CropCosts
            .AsNoTracking()
            .Where(x => x.CostDate >= from && x.CostDate < toExclusive);

        if (filter.CostTypeIds != null && filter.CostTypeIds.Length > 0)
            baseQuery = baseQuery.Where(x => filter.CostTypeIds.Contains(x.CostTypeId));

        if (filter.CropIds != null && filter.CropIds.Length > 0)
            baseQuery = baseQuery.Where(x => filter.CropIds.Contains(x.CropId));

        if (filter.LocationIds != null && filter.LocationIds.Length > 0)
            baseQuery = baseQuery.Where(x => filter.LocationIds.Contains(x.Crop.LocationId));

        if (filter.CropTypeIds != null && filter.CropTypeIds.Length > 0)
            baseQuery = baseQuery.Where(x => filter.CropTypeIds.Contains(x.Crop.CropTypeId));

        var agg = await baseQuery.GroupBy(x => 1)
            .Select(g => new
            {
                TotalAmount = g.Sum(i => i.TotalAmount),
                TotalQuantity = g.Sum(i => i.Quantity),
                TransactionCount = g.Count()
            })
            .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

        return (agg?.TotalAmount ?? 0m, agg?.TotalQuantity ?? 0m, agg?.TransactionCount ?? 0);
    }

    public async Task<IEnumerable<CropCostDetailDto>> GetExportRowsAsync(CropCostReportFilterDto filter, CancellationToken cancellationToken = default)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));
        if (filter.FromDate > filter.ToDate) throw new ArgumentException("FromDate must be <= ToDate");

        DateTime from = filter.FromDate.Date;
        DateTime toExclusive = filter.ToDate.Date.AddDays(1);

        var query = _context.CropCosts
            .AsNoTracking()
            .Where(x => x.CostDate >= from && x.CostDate < toExclusive);

        if (filter.CostTypeIds != null && filter.CostTypeIds.Length > 0)
            query = query.Where(x => filter.CostTypeIds.Contains(x.CostTypeId));

        if (filter.CropIds != null && filter.CropIds.Length > 0)
            query = query.Where(x => filter.CropIds.Contains(x.CropId));

        if (filter.LocationIds != null && filter.LocationIds.Length > 0)
            query = query.Where(x => filter.LocationIds.Contains(x.Crop.LocationId));

        if (filter.CropTypeIds != null && filter.CropTypeIds.Length > 0)
            query = query.Where(x => filter.CropTypeIds.Contains(x.Crop.CropTypeId));

        var rows = await query
            .OrderBy(x => x.CostDate)
            .Select(x => new CropCostDetailDto
            {
                CropCostId = x.Id,
                CostDate = x.CostDate,
                CropId = x.CropId,
                CropName = x.Crop.Name,
                CostTypeId = x.CostTypeId,
                CostTypeName = x.CostType.Name,
                LocationId = x.Crop.LocationId,
                LocationName = x.Crop.Location.Name,
                Quantity = x.Quantity,
                Unit = x.Unit,
                UnitPrice = x.UnitPrice,
                TotalAmount = x.TotalAmount,
                Note = x.Note,
                CreatedDate = x.CreatedDate
            })
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return rows;
    }
}
