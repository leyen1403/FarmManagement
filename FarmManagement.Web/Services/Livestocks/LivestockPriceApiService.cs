using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Livestocks;

public class LivestockPriceApiService
{
    private readonly HttpClient _http;

    public LivestockPriceApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<LivestockPriceDto>> GetByLivestockIdAsync(int livestockId)
    {
        return await _http.GetFromJsonAsync<List<LivestockPriceDto>>($"api/LivestockPrices/livestock/{livestockId}") ?? new();
    }

    public async Task<LivestockPriceDto?> GetByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<LivestockPriceDto>($"api/LivestockPrices/{id}");
    }

    public async Task<LivestockPriceDto?> GetActivePriceAsync(int livestockId, int gender, DateTime? at = null)
    {
        var q = $"api/LivestockPrices/active?livestockId={livestockId}&gender={gender}";
        if (at.HasValue)
        {
            q += $"&at={at.Value:O}";
        }
        return await _http.GetFromJsonAsync<LivestockPriceDto?>(q);
    }

    public async Task<int> CreateAsync(LivestockPriceDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/LivestockPrices", dto);
        await EnsureSuccess(response);
        return 0;
    }

    public async Task UpdateAsync(int id, LivestockPriceDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/LivestockPrices/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/LivestockPrices/{id}");
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