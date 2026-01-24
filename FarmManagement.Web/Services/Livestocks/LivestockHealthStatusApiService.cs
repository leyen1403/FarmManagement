using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Livestocks;

public class LivestockHealthStatusApiService
{
    private readonly HttpClient _http;

    public LivestockHealthStatusApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<LivestockHealthStatusDto>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<LivestockHealthStatusDto>>("api/LivestockHealthStatuses") ?? new();
    }

    public async Task<LivestockHealthStatusDto?> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<LivestockHealthStatusDto>($"api/LivestockHealthStatuses/{id}");

    public async Task<int> CreateAsync(CreateLivestockHealthStatusDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/LivestockHealthStatuses", dto);
        await EnsureSuccess(response);
        return 0;
    }

    public async Task UpdateAsync(int id, UpdateLivestockHealthStatusDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/LivestockHealthStatuses/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/LivestockHealthStatuses/{id}");
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
