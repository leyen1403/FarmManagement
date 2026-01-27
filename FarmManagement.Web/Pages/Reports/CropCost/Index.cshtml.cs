using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FarmManagement.Application.DTOs.Reports.CropCost;
using FarmManagement.Web.Models.Reports.CropCost;
using FarmManagement.Web.Services.Api.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FarmManagement.Web.Pages.Reports.CropCost
{
    public class IndexModel : PageModel
    {
        private readonly CropCostReportApiService _apiClient;
        private readonly ILogger<IndexModel> _logger;

        private const int MaxPageSize = 1000;

        public IndexModel(CropCostReportApiService apiClient, ILogger<IndexModel> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
            PageVm = new CropCostReportPageVm();
        }

        [BindProperty(SupportsGet = true)]
        public CropCostReportFilterDto Filter { get; set; } = new CropCostReportFilterDto
        {
            FromDate = DateTime.UtcNow.Date.AddDays(-30),
            ToDate = DateTime.UtcNow.Date,
            Page = 1,
            PageSize = 50
        };

        public CropCostReportPageVm PageVm { get; set; }

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
        {
            try
            {
                if (Filter == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid filter");
                    return Page();
                }

                if (Filter.FromDate > Filter.ToDate)
                {
                    ModelState.AddModelError("Filter.FromDate", "Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.");
                    return Page();
                }

                if (Filter.PageSize <= 0) Filter.PageSize = 50;
                if (Filter.Page <= 0) Filter.Page = 1;
                if (Filter.PageSize > MaxPageSize) Filter.PageSize = MaxPageSize;

                var summary = await _apiClient.GetSummaryAsync(Filter).ConfigureAwait(false);

                // Map to UI ViewModel
                PageVm.Filter = new CropCostReportFilterVm
                {
                    FromDate = Filter.FromDate,
                    ToDate = Filter.ToDate,
                    TimeGrain = Filter.TimeGrain.ToString(),
                    CostTypeIds = Filter.CostTypeIds,
                    CropIds = Filter.CropIds,
                    LocationIds = Filter.LocationIds,
                    CropTypeIds = Filter.CropTypeIds,
                    IncludeInactive = Filter.IncludeInactive,
                    Page = Filter.Page,
                    PageSize = Filter.PageSize
                };

                PageVm.SummaryRows = summary.Rows.Select(r => new CropCostSummaryRowVm
                {
                    TimePeriodKey = r.TimePeriodKey,
                    CostTypeName = r.CostTypeName,
                    CropName = r.CropName,
                    LocationName = r.LocationName,
                    TotalAmount = r.TotalAmount,
                    TotalQuantity = r.TotalQuantity,
                    TransactionCount = r.TransactionCount
                }).ToList();

                PageVm.OverallTotals = new CropCostOverallTotalsVm
                {
                    TotalAmount = summary.TotalAmount,
                    TotalQuantity = summary.TotalQuantity,
                    TransactionCount = summary.TransactionCount
                };

                PageVm.Paging = new PagingInfoVm
                {
                    Page = summary.Page,
                    PageSize = summary.PageSize,
                    TotalCount = summary.TotalCount,
                    HasMore = summary.HasMore
                };

                return Page();
            }
            catch (OperationCanceledException)
            {
                return new StatusCodeResult(499);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load CropCost report via API");
                ErrorMessage = "Không thể tải báo cáo. Vui lòng thử lại sau.";
                return Page();
            }
        }

        public async Task<IActionResult> OnGetDetailsAsync(
            string? timePeriodKey,
            int? costTypeId,
            int? cropId,
            int? locationId,
            int? cropTypeId,
            int? page,
            int? pageSize,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (page.HasValue && page.Value > 0) Filter.Page = page.Value;
                if (pageSize.HasValue && pageSize.Value > 0) Filter.PageSize = Math.Min(pageSize.Value, MaxPageSize);

                var request = new CropCostReportDetailRequestDto
                {
                    Filter = Filter,
                    TimePeriodKey = timePeriodKey,
                    CostTypeId = costTypeId,
                    CropId = cropId,
                    LocationId = locationId,
                    CropTypeId = cropTypeId
                };

                var details = await _apiClient.GetDetailsAsync(request).ConfigureAwait(false);

                // Map detail rows for UI if needed
                PageVm.DetailPagedResult = details;

                return new JsonResult(details);
            }
            catch (OperationCanceledException)
            {
                return new StatusCodeResult(499);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load CropCost detail via API");
                return StatusCode(500, new { error = "Không thể tải chi tiết. Vui lòng thử lại sau." });
            }
        }

        public async Task<IActionResult> OnPostExportAsync(CancellationToken cancellationToken)
        {
            try
            {
                if (Filter == null) return BadRequest();
                if (Filter.FromDate > Filter.ToDate)
                {
                    ModelState.AddModelError("Filter.FromDate", "Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.");
                    return Page();
                }

                var stream = await _apiClient.ExportAsync(Filter).ConfigureAwait(false);
                if (stream == null) return StatusCode(500, "Không thể tạo file export.");

                if (stream.CanSeek) stream.Position = 0;
                string fileName = $"crop-cost-report-{DateTime.UtcNow:yyyyMMddHHmmss}.csv";
                const string contentType = "text/csv";

                return File(stream, contentType, fileName);
            }
            catch (OperationCanceledException)
            {
                return new StatusCodeResult(499);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to export CropCost report via API");
                return StatusCode(500, "Không thể xuất báo cáo. Vui lòng thử lại sau.");
            }
        }
    }
}
