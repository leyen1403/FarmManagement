using System.Net.Http.Json;
using FarmManagement.Application.DTOs.Reports.CropCost;

namespace FarmManagement.Web.Services.Api.Reports;

public class CropCostReportApiService
{
    private readonly HttpClient _http;

    // Use typed HttpClient injected by IHttpClientFactory via AddHttpClient<T>
    public CropCostReportApiService(HttpClient httpClient)
    {
        _http = httpClient;
    }

    public async Task<CropCostSummaryPagedResultDto> GetSummaryAsync(CropCostReportFilterDto filter)
    {
        var url = QueryStringFromFilter("api/reports/cropcost/summary", filter);
        return await _http.GetFromJsonAsync<CropCostSummaryPagedResultDto>(url) ?? new CropCostSummaryPagedResultDto();
    }

    public async Task<CropCostDetailPagedResultDto> GetDetailsAsync(CropCostReportDetailRequestDto request)
    {
        // Build URL with filter + group keys
        var url = QueryStringFromFilter("api/reports/cropcost/details", request.Filter);
        var qs = new List<string>();
        if (!string.IsNullOrEmpty(request.TimePeriodKey)) qs.Add($"timePeriodKey={Uri.EscapeDataString(request.TimePeriodKey)}");
        if (request.CostTypeId.HasValue) qs.Add($"costTypeId={request.CostTypeId.Value}");
        if (request.CropId.HasValue) qs.Add($"cropId={request.CropId.Value}");
        if (request.LocationId.HasValue) qs.Add($"locationId={request.LocationId.Value}");
        if (request.CropTypeId.HasValue) qs.Add($"cropTypeId={request.CropTypeId.Value}");

        if (qs.Count > 0) url += "&" + string.Join("&", qs);

        return await _http.GetFromJsonAsync<CropCostDetailPagedResultDto>(url) ?? new CropCostDetailPagedResultDto();
    }

    public async Task<Stream> ExportAsync(CropCostReportFilterDto filter)
    {
        var response = await _http.PostAsJsonAsync("api/reports/cropcost/export", filter);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStreamAsync();
    }

    private string QueryStringFromFilter(string baseUrl, CropCostReportFilterDto filter)
    {
        var pairs = new List<string>();
        pairs.Add($"FromDate={Uri.EscapeDataString(filter.FromDate.ToString("yyyy-MM-dd"))}");
        pairs.Add($"ToDate={Uri.EscapeDataString(filter.ToDate.ToString("yyyy-MM-dd"))}");
        pairs.Add($"TimeGrain={filter.TimeGrain}");
        if (filter.GroupBy != null)
        {
            foreach (var g in filter.GroupBy) pairs.Add($"GroupBy={g}");
        }
        if (filter.CostTypeIds != null) foreach (var id in filter.CostTypeIds) pairs.Add($"CostTypeIds={id}");
        if (filter.CropIds != null) foreach (var id in filter.CropIds) pairs.Add($"CropIds={id}");
        if (filter.LocationIds != null) foreach (var id in filter.LocationIds) pairs.Add($"LocationIds={id}");
        if (filter.CropTypeIds != null) foreach (var id in filter.CropTypeIds) pairs.Add($"CropTypeIds={id}");
        pairs.Add($"IncludeInactive={filter.IncludeInactive}");
        if (filter.MinTotalAmount.HasValue) pairs.Add($"MinTotalAmount={filter.MinTotalAmount.Value}");
        if (filter.MaxTotalAmount.HasValue) pairs.Add($"MaxTotalAmount={filter.MaxTotalAmount.Value}");
        if (filter.TopN.HasValue) pairs.Add($"TopN={filter.TopN.Value}");
        pairs.Add($"Page={filter.Page}");
        pairs.Add($"PageSize={filter.PageSize}");
        if (filter.SortBy.HasValue) pairs.Add($"SortBy={filter.SortBy.Value}");
        pairs.Add($"SortDirection={filter.SortDirection}");

        return baseUrl + "?" + string.Join("&", pairs);
    }
}
