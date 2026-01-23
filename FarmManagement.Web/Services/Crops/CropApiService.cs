using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Crops;

public class CropApiService
{
    private readonly HttpClient _http;

    public CropApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<CropDto>> GetAllAsync(int? cropTypeId = null, int? cropStatusId = null, int? locationId = null)
    {
        var queryParams = new List<string>();
        if (cropTypeId.HasValue)
            queryParams.Add($"cropTypeId={cropTypeId}");
        if (cropStatusId.HasValue)
            queryParams.Add($"cropStatusId={cropStatusId}");
        if (locationId.HasValue)
            queryParams.Add($"locationId={locationId}");

        var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
        return await _http.GetFromJsonAsync<List<CropDto>>($"api/Crop{queryString}") ?? new();
    }

    public async Task<CropDto?> GetByIdAsync(int id)
           => await _http.GetFromJsonAsync<CropDto>($"api/Crop/{id}");

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

    public async Task<int> CreateAsync(CreateCropDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/Crop", dto);
        await EnsureSuccess(response);
        var result = await response.Content.ReadFromJsonAsync<CreateResponse>();
        return result?.Id ?? 0;
    }

    public async Task UpdateAsync(int id, UpdateCropDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/Crop/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/Crop/{id}");
        await EnsureSuccess(response);
    }

    private class CreateResponse
    {
        public int Id { get; set; }
    }
}
