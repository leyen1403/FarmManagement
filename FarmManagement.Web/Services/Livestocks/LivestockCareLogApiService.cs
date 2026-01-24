using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Livestocks;

public class LivestockCareLogApiService
{
    private readonly HttpClient _http;

    public LivestockCareLogApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<LivestockCareLogDto>> GetByLivestockIdAsync(int livestockId)
    {
        return await _http.GetFromJsonAsync<List<LivestockCareLogDto>>($"api/LivestockCareLogs/livestock/{livestockId}") ?? new();
    }

    public async Task<LivestockCareLogDto?> GetByIdAsync(int id)
  => await _http.GetFromJsonAsync<LivestockCareLogDto>($"api/LivestockCareLogs/{id}");

    public async Task<int> CreateAsync(CreateLivestockCareLogDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/LivestockCareLogs", dto);
        await EnsureSuccess(response);
        return 0;
    }

    public async Task UpdateAsync(int id, UpdateLivestockCareLogDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/LivestockCareLogs/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/LivestockCareLogs/{id}");
        await EnsureSuccess(response);
    }

    public async Task<List<LivestockCareTypeDto>> GetCareTypesAsync()
    {
        return await _http.GetFromJsonAsync<List<LivestockCareTypeDto>>("api/LivestockCareTypes") ?? new();
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
