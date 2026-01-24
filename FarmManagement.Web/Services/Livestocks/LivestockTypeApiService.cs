using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Livestocks;

public class LivestockTypeApiService
{
    private readonly HttpClient _http;

    public LivestockTypeApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<LivestockTypeDto>> GetAllAsync(bool includeInactive = false)
    {
        return await _http.GetFromJsonAsync<List<LivestockTypeDto>>($"api/LivestockTypes?includeInactive={includeInactive}") ?? new();
    }

    public async Task<LivestockTypeDto?> GetByIdAsync(int id)
     => await _http.GetFromJsonAsync<LivestockTypeDto>($"api/LivestockTypes/{id}");

    public async Task<int> CreateAsync(CreateLivestockTypeDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/LivestockTypes", dto);
        await EnsureSuccess(response);
        return 0;
    }

    public async Task UpdateAsync(int id, UpdateLivestockTypeDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/LivestockTypes/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/LivestockTypes/{id}");
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
