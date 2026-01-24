using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Livestocks;

public class LivestockStatusApiService
{
    private readonly HttpClient _http;

    public LivestockStatusApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<LivestockStatusDto>> GetAllAsync(bool includeInactive = false)
    {
        return await _http.GetFromJsonAsync<List<LivestockStatusDto>>($"api/LivestockStatuses?includeInactive={includeInactive}") ?? new();
    }

    public async Task<LivestockStatusDto?> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<LivestockStatusDto>($"api/LivestockStatuses/{id}");

    public async Task<int> CreateAsync(CreateLivestockStatusDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/LivestockStatuses", dto);
        await EnsureSuccess(response);
        return 0;
    }

    public async Task UpdateAsync(int id, UpdateLivestockStatusDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/LivestockStatuses/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/LivestockStatuses/{id}");
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
