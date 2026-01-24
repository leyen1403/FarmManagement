using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Livestocks;

public class LivestockSaleApiService
{
    private readonly HttpClient _http;

    public LivestockSaleApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<LivestockSaleDto>> GetByLivestockIdAsync(int livestockId)
    {
        return await _http.GetFromJsonAsync<List<LivestockSaleDto>>($"api/LivestockSales/livestock/{livestockId}") ?? new();
    }

    public async Task<LivestockSaleDto?> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<LivestockSaleDto>($"api/LivestockSales/{id}");

    public async Task<int> CreateAsync(CreateLivestockSaleDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/LivestockSales", dto);
        await EnsureSuccess(response);
        return 0;
    }

    public async Task UpdateAsync(int id, UpdateLivestockSaleDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/LivestockSales/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/LivestockSales/{id}");
        await EnsureSuccess(response);
    }

    public async Task<List<SaleTypeDto>> GetSaleTypesAsync()
    {
        return await _http.GetFromJsonAsync<List<SaleTypeDto>>("api/SaleTypes") ?? new();
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