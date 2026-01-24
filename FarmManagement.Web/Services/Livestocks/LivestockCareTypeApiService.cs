using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Livestocks;

public class LivestockCareTypeApiService
{
    private readonly HttpClient _http;

    public LivestockCareTypeApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<LivestockCareTypeDto>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<LivestockCareTypeDto>>("api/LivestockCareTypes") ?? new();
    }

    public async Task<LivestockCareTypeDto?> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<LivestockCareTypeDto>($"api/LivestockCareTypes/{id}");

    public async Task<int> CreateAsync(CreateLivestockCareTypeDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/LivestockCareTypes", dto);
        await EnsureSuccess(response);
        return 0;
    }

    public async Task UpdateAsync(int id, UpdateLivestockCareTypeDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/LivestockCareTypes/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/LivestockCareTypes/{id}");
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