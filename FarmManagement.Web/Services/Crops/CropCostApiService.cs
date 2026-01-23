using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Crops;

public class CropCostApiService
{
    private readonly HttpClient _http;

    public CropCostApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<CropCostDto>> GetByCropIdAsync(int cropId)
   => await _http.GetFromJsonAsync<List<CropCostDto>>($"api/CropCost/crop/{cropId}") ?? new();

    public async Task<CropCostDto?> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<CropCostDto>($"api/CropCost/{id}");

    public async Task<List<CostTypeDto>> GetCostTypesAsync()
     => await _http.GetFromJsonAsync<List<CostTypeDto>>("api/CostType") ?? new();

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

    public async Task<int> CreateAsync(CreateCropCostDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/CropCost", dto);
        await EnsureSuccess(response);
        var result = await response.Content.ReadFromJsonAsync<CreateResponse>();
        return result?.Id ?? 0;
    }

    public async Task UpdateAsync(int id, UpdateCropCostDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/CropCost/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/CropCost/{id}");
        await EnsureSuccess(response);
    }

    private class CreateResponse
    {
        public int Id { get; set; }
    }
}
