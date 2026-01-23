using FarmManagement.Application.DTOs.Locations;
using Microsoft.AspNetCore.Mvc;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Services.Locations;

namespace FarmManagement.Web.Controllers.Locations;

public class LocationController : Controller
{
    private readonly LocationApiClient _api;
    private readonly LocationTypeApiClient _typeApi;
    private readonly LocationStatusApiClient _statusApi;

    public LocationController(LocationApiClient api, LocationTypeApiClient typeApi, LocationStatusApiClient statusApi)
    {
        _api = api;
        _typeApi = typeApi;
        _statusApi = statusApi;
    }

    public async Task<IActionResult> Index() => View(await _api.GetAllAsync());

    public async Task<IActionResult> Create()
    {
        await LoadDropdowns();
        return View();
    }

    private async Task LoadDropdowns()
    {
        ViewBag.Types = await _typeApi.GetAllAsync();
        ViewBag.Statuses = await _statusApi.GetAllAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Create(LocationDto dto)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdowns();
            return View(dto);
        }

        try
        {
            await _api.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        catch (ApiException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }

        await LoadDropdowns();
        return View(dto);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var entity = await _api.GetByIdAsync(id);
        if (entity == null)
            return NotFound();

        await LoadDropdowns();
        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, LocationDto dto)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdowns();
            return View(dto);
        }

        try
        {
            await _api.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }
        catch (ApiException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }

        await LoadDropdowns();
        return View(dto);
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _api.DeleteAsync(id);
        }
        catch (ApiException ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}