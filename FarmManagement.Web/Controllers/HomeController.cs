using FarmManagement.Web.Models;
using FarmManagement.Web.Services.Common;
using FarmManagement.Web.Services.Locations;
using FarmManagement.Web.Services.Crops;
using FarmManagement.Web.Services.Livestocks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FarmManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ActivityLogApiService _activityLogService;
        private readonly LocationApiClient _locationService;
        private readonly CropTypeApiService _cropTypeService;
        private readonly CropStatusApiService _cropStatusService;
        private readonly LivestockApiService _livestockService;
        private readonly LivestockTypeApiService _livestockTypeService;

        public HomeController(
            ILogger<HomeController> logger,
            ActivityLogApiService activityLogService,
            LocationApiClient locationService,
            CropTypeApiService cropTypeService,
            CropStatusApiService cropStatusService,
            LivestockApiService livestockService,
            LivestockTypeApiService livestockTypeService)
        {
            _logger = logger;
            _activityLogService = activityLogService;
            _locationService = locationService;
            _cropTypeService = cropTypeService;
            _cropStatusService = cropStatusService;
            _livestockService = livestockService;
            _livestockTypeService = livestockTypeService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Load statistics
                var locations = await _locationService.GetAllAsync();
                var cropTypes = await _cropTypeService.GetAllAsync(false);
                var cropStatuses = await _cropStatusService.GetAllAsync(false);
                var livestocks = await _livestockService.GetAllAsync();
                var livestockTypes = await _livestockTypeService.GetAllAsync(false);
                var activityCountToday = await _activityLogService.GetActivityCountTodayAsync();
                var recentActivities = await _activityLogService.GetRecentActivitiesAsync(10);

                ViewBag.LocationCount = locations?.Count ?? 0;
                ViewBag.CropTypeCount = cropTypes?.Count() ?? 0;
                ViewBag.CropStatusCount = cropStatuses?.Count() ?? 0;
                ViewBag.LivestockCount = livestocks?.Count ?? 0;
                ViewBag.LivestockTypeCount = livestockTypes?.Count ?? 0;
                ViewBag.ActivityCountToday = activityCountToday;
                ViewBag.RecentActivities = recentActivities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dashboard data");
                ViewBag.LocationCount = 0;
                ViewBag.CropTypeCount = 0;
                ViewBag.CropStatusCount = 0;
                ViewBag.LivestockCount = 0;
                ViewBag.LivestockTypeCount = 0;
                ViewBag.ActivityCountToday = 0;
                ViewBag.RecentActivities = new List<FarmManagement.Application.DTOs.Common.ActivityLogDto>();
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
