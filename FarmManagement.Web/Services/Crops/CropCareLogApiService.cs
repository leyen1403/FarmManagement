using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Crops;

public class CropCareLogApiService
{
    private readonly HttpClient _http;

    public CropCareLogApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<CropCareLogDto>> GetByCropIdAsync(int cropId)
        => await _http.GetFromJsonAsync<List<CropCareLogDto>>($"api/CropCareLog/crop/{cropId}") ?? new();

    public async Task<CropCareLogDto?> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<CropCareLogDto>($"api/CropCareLog/{id}");

    public async Task<List<CropCareTypeDto>> GetCareTypesAsync()
        => await _http.GetFromJsonAsync<List<CropCareTypeDto>>("api/CropCareType") ?? new();

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

    public async Task<int> CreateAsync(CreateCropCareLogDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/CropCareLog", dto);
        await EnsureSuccess(response);
        var result = await response.Content.ReadFromJsonAsync<CreateResponse>();
        return result?.Id ?? 0;
    }

    public async Task UpdateAsync(int id, UpdateCropCareLogDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/CropCareLog/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/CropCareLog/{id}");
        await EnsureSuccess(response);
    }

    private class CreateResponse
    {
        public int Id { get; set; }
    }
}