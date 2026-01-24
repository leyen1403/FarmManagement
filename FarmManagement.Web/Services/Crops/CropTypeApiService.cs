using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Crops;

public class CropTypeApiService
{
    private readonly HttpClient _http;

    public CropTypeApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<CropTypeDto>> GetAllAsync(bool includeInactive = true)
        => await _http.GetFromJsonAsync<List<CropTypeDto>>($"api/CropType?includeInactive={includeInactive}")
           ?? new();

    public async Task<CropTypeDto> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<CropTypeDto>($"api/CropType/{id}")
           ?? throw new ApiException("Không tìm thấy loại cây trồng", 404);

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

    public async Task CreateAsync(CreateCropTypeDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/CropType", dto);
        await EnsureSuccess(response);
    }

    public async Task UpdateAsync(int id, UpdateCropTypeDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/CropType/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/CropType/{id}");
        await EnsureSuccess(response);
    }
}