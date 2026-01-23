using FarmManagement.Application.DTOs.Common;

namespace FarmManagement.Web.Services.Common;

public class ActivityLogApiService
{
    private readonly HttpClient _http;

    public ActivityLogApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<ActivityLogDto>> GetRecentActivitiesAsync(int count = 10)
    {
        var result = await _http.GetFromJsonAsync<List<ActivityLogDto>>($"api/ActivityLogs/recent?count={count}");
        return result ?? new List<ActivityLogDto>();
    }

    public async Task<int> GetActivityCountTodayAsync()
    {
        var result = await _http.GetFromJsonAsync<ActivityCountResponse>("api/ActivityLogs/count-today");
        return result?.Count ?? 0;
    }

    private class ActivityCountResponse
    {
        public int Count { get; set; }
    }
}
