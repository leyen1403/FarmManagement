using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Livestocks;

public class LivestockHealthLogApiService
{
    private readonly HttpClient _http;

    public LivestockHealthLogApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<LivestockHealthLogDto>> GetByLivestockIdAsync(int livestockId)
    {
        return await _http.GetFromJsonAsync<List<LivestockHealthLogDto>>($"api/LivestockHealthLogs/livestock/{livestockId}") ?? new();
    }

    public async Task<LivestockHealthLogDto?> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<LivestockHealthLogDto>($"api/LivestockHealthLogs/{id}");

    public async Task<int> CreateAsync(CreateLivestockHealthLogDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/LivestockHealthLogs", dto);
        await EnsureSuccess(response);
        return 0;
    }

    public async Task UpdateAsync(int id, UpdateLivestockHealthLogDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/LivestockHealthLogs/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/LivestockHealthLogs/{id}");
        await EnsureSuccess(response);
    }

    public async Task<List<LivestockHealthStatusDto>> GetHealthStatusesAsync()
    {
        return await _http.GetFromJsonAsync<List<LivestockHealthStatusDto>>("api/LivestockHealthStatuses") ?? new();
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