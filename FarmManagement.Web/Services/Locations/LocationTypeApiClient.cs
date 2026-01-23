using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Models;

namespace FarmManagement.Web.Services.Locations;

public class LocationTypeApiClient
{
    private readonly HttpClient _http;

    public LocationTypeApiClient(IHttpClientFactory factory) => _http = factory.CreateClient("FarmApi");

    public async Task<List<LocationTypeDto>> GetAllAsync() =>
    await _http.GetFromJsonAsync<List<LocationTypeDto>>("api/LocationTypes");

    public async Task<LocationTypeDto> GetByIdAsync(int id) =>
    await _http.GetFromJsonAsync<LocationTypeDto>($"api/LocationTypes/{id}");

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

    public async Task CreateAsync(LocationTypeDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/LocationTypes", dto);
        await EnsureSuccess(response);
    }

    public async Task UpdateAsync(int id, LocationTypeDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/LocationTypes/{id}", dto);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/LocationTypes/{id}");
        await EnsureSuccess(response);
    }
}