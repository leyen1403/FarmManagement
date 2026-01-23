using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Locations;

public class LocationApiClient
{
    private readonly HttpClient _http;

    public LocationApiClient(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("FarmApi");
    }

    public async Task<List<LocationDto>> GetAllAsync(bool activeOnly = false) =>
        await _http.GetFromJsonAsync<List<LocationDto>>($"api/Locations?activeOnly={activeOnly}");

    public async Task<LocationDto> GetByIdAsync(int id) =>
    await _http.GetFromJsonAsync<LocationDto>($"api/Locations/{id}");

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

    public async Task CreateAsync(LocationDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/Locations", dto);
        await EnsureSuccess(response);
    }

    public async Task UpdateAsync(int id, LocationDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/Locations/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/Locations/{id}");
        await EnsureSuccess(response);
    }
}