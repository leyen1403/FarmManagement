using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Crops;

public class CostTypeApiService
{
    private readonly HttpClient _http;

    public CostTypeApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<CostTypeDto>> GetAllAsync(bool includeInactive = true)
        => await _http.GetFromJsonAsync<List<CostTypeDto>>($"api/CostType?includeInactive={includeInactive}")
           ?? new();

    public async Task<CostTypeDto> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<CostTypeDto>($"api/CostType/{id}")
           ?? throw new ApiException("Không tìm thấy loại chi phí", 404);

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

    public async Task CreateAsync(CreateCostTypeDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/CostType", dto);
        await EnsureSuccess(response);
    }

    public async Task UpdateAsync(int id, UpdateCostTypeDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/CostType/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/CostType/{id}");
        await EnsureSuccess(response);
    }
}