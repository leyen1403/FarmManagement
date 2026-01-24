using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Livestocks;

public class LivestockApiService
{
    private readonly HttpClient _http;

    public LivestockApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<LivestockDto>> GetAllAsync(int? livestockTypeId = null, int? livestockStatusId = null, int? locationId = null)
    {
        var queryParams = new List<string>();
        if (livestockTypeId.HasValue)
            queryParams.Add($"livestockTypeId={livestockTypeId}");
        if (livestockStatusId.HasValue)
            queryParams.Add($"livestockStatusId={livestockStatusId}");
        if (locationId.HasValue)
            queryParams.Add($"locationId={locationId}");

        var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
        return await _http.GetFromJsonAsync<List<LivestockDto>>($"api/Livestocks{queryString}") ?? new();
    }

    public async Task<LivestockDto?> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<LivestockDto>($"api/Livestocks/{id}");

    public async Task<int> CreateAsync(CreateLivestockDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/Livestocks", dto);
        await EnsureSuccess(response);

        // CreatedAtAction trả về location header, trích xuất ID từ URL
        if (response.Headers.Location != null)
        {
            var segments = response.Headers.Location.Segments;
            if (segments.Length > 0 && int.TryParse(segments[^1], out var id))
                return id;
        }
        return 0;
    }

    public async Task UpdateAsync(int id, UpdateLivestockDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/Livestocks/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/Livestocks/{id}");
        await EnsureSuccess(response);
    }

    private async Task EnsureSuccess(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return;

        var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
        throw new ApiException(
            error?.Message ?? "Lỗi hệ thống",
            (int)response.StatusCode
        );
    }
}