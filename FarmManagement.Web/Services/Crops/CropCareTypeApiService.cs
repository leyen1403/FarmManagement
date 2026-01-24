using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Crops;

public class CropCareTypeApiService
{
    private readonly HttpClient _http;

    public CropCareTypeApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<CropCareTypeDto>> GetAllAsync(bool includeInactive = true)
        => await _http.GetFromJsonAsync<List<CropCareTypeDto>>($"api/CropCareType?includeInactive={includeInactive}")
           ?? new();

    public async Task<CropCareTypeDto> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<CropCareTypeDto>($"api/CropCareType/{id}")
           ?? throw new ApiException("Không tìm thấy loại chăm sóc", 404);

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

    public async Task CreateAsync(CreateCropCareTypeDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/CropCareType", dto);
        await EnsureSuccess(response);
    }

    public async Task UpdateAsync(int id, UpdateCropCareTypeDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/CropCareType/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/CropCareType/{id}");
        await EnsureSuccess(response);
    }
}