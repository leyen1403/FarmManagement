using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Locations;

public class LocationStatusApiClient
{
    private readonly HttpClient _http;

    public LocationStatusApiClient(IHttpClientFactory factory)
        => _http = factory.CreateClient("FarmApi");

    public async Task<List<LocationStatusDto>> GetAllAsync()
        => await _http.GetFromJsonAsync<List<LocationStatusDto>>("api/LocationStatuses") ?? new();

    public async Task<LocationStatusDto?> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<LocationStatusDto>($"api/LocationStatuses/{id}");

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

    public async Task CreateAsync(LocationStatusDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/LocationStatuses", dto);
        await EnsureSuccess(response);
    }

    public async Task UpdateAsync(int id, LocationStatusDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/LocationStatuses/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/LocationStatuses/{id}");
        await EnsureSuccess(response);
    }
}