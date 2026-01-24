using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Livestocks;

public class SaleTypeApiService
{
    private readonly HttpClient _http;

    public SaleTypeApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<SaleTypeDto>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<SaleTypeDto>>("api/SaleTypes") ?? new();
    }

    public async Task<SaleTypeDto?> GetByIdAsync(int id)
     => await _http.GetFromJsonAsync<SaleTypeDto>($"api/SaleTypes/{id}");

    public async Task<int> CreateAsync(CreateSaleTypeDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/SaleTypes", dto);
        await EnsureSuccess(response);
        return 0;
    }

    public async Task UpdateAsync(int id, UpdateSaleTypeDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/SaleTypes/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/SaleTypes/{id}");
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
