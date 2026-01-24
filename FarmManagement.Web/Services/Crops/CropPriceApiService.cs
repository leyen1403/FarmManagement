using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Crops;

public class CropPriceApiService
{
    private readonly HttpClient _http;

    public CropPriceApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<CropPriceDto>> GetAllAsync(int? cropId = null, bool includeInactive = false)
    {
        var queryParams = new List<string>();
        if (cropId.HasValue)
            queryParams.Add($"cropId={cropId}");
        queryParams.Add($"includeInactive={includeInactive}");

        var queryString = "?" + string.Join("&", queryParams);
        return await _http.GetFromJsonAsync<List<CropPriceDto>>($"api/CropPrice{queryString}") ?? new();
    }

    public async Task<CropPriceDto?> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<CropPriceDto>($"api/CropPrice/{id}");

    public async Task<List<CropPriceDto>> GetPriceHistoryAsync(int cropId)
        => await _http.GetFromJsonAsync<List<CropPriceDto>>($"api/CropPrice/history/{cropId}") ?? new();

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

    public async Task<int> CreateAsync(CreateCropPriceDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/CropPrice", dto);
        await EnsureSuccess(response);
        var result = await response.Content.ReadFromJsonAsync<CreateResponse>();
        return result?.Id ?? 0;
    }

    public async Task UpdateAsync(int id, UpdateCropPriceDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/CropPrice/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/CropPrice/{id}");
        await EnsureSuccess(response);
    }

    private class CreateResponse
    {
        public int Id { get; set; }
    }
}