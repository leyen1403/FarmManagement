using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Web.Services.Crops;

public class CropStatusApiService
{
    private readonly HttpClient _httpClient;

    public CropStatusApiService(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient.CreateClient("FarmApi");
    }

    public async Task<IEnumerable<CropStatusDto>> GetAllAsync(bool includeInactive)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<CropStatusDto>>($"api/CropStatus?includeInactive={includeInactive}")
               ?? new List<CropStatusDto>();
    }

    public async Task<CropStatusDto> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<CropStatusDto>($"api/CropStatus/{id}")
               ?? throw new InvalidOperationException("Response was null");
    }

    public async Task CreateAsync(CreateCropStatusDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/CropStatus", dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateAsync(int id, UpdateCropStatusDto dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/CropStatus/{id}", dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/CropStatus/{id}");
        response.EnsureSuccessStatusCode();
    }
}