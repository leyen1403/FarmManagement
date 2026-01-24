using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Crops;

public class CropHarvestApiService
{
    private readonly HttpClient _http;

    public CropHarvestApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<CropHarvestDto>> GetByCropIdAsync(int cropId)
        => await _http.GetFromJsonAsync<List<CropHarvestDto>>($"api/CropHarvest/crop/{cropId}") ?? new();

    public async Task<CropHarvestDto?> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<CropHarvestDto>($"api/CropHarvest/{id}");

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

    public async Task<int> CreateAsync(CreateCropHarvestDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/CropHarvest", dto);
        await EnsureSuccess(response);
        var result = await response.Content.ReadFromJsonAsync<CreateResponse>();
        return result?.Id ?? 0;
    }

    public async Task UpdateAsync(int id, UpdateCropHarvestDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/CropHarvest/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/CropHarvest/{id}");
        await EnsureSuccess(response);
    }

    private class CreateResponse
    {
        public int Id { get; set; }
    }
}