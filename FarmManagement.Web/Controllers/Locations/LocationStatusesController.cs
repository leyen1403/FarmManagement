using FarmManagement.Application.DTOs.Locations;
using FarmManagement.Web.Exceptions;
using FarmManagement.Web.Services.Locations;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Web.Controllers.Locations;

public class LocationStatusesController : Controller
{
    private readonly LocationStatusApiClient _api;

    public LocationStatusesController(LocationStatusApiClient api) => _api = api;

    public async Task<IActionResult> Index() => View(await _api.GetAllAsync());

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(LocationStatusDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            await _api.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        catch (ApiException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(dto);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var entity = await _api.GetByIdAsync(id);
        if (entity == null)
            return NotFound();
        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, LocationStatusDto dto)
    {
        if (id != dto.Id)
            return BadRequest("Id mismatch");

        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            await _api.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }
        catch (ApiException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(dto);
        }
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