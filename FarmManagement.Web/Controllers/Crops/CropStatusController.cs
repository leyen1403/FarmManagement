using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Web.Services.Crops;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Web.Controllers.Crops;

public class CropStatusController : Controller
{
    private readonly CropStatusApiService _apiService;

    public CropStatusController(CropStatusApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IActionResult> Index()
    {
        var cropStatuses = await _apiService.GetAllAsync(includeInactive: true);
        return View(cropStatuses);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateCropStatusDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            await _apiService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(dto);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var cropStatus = await _apiService.GetByIdAsync(id);
        var updateDto = new UpdateCropStatusDto
        {
            Id = cropStatus.Id,
            Code = cropStatus.Code,
            Name = cropStatus.Name,
            Description = cropStatus.Description,
            IsActive = cropStatus.IsActive
        };
        return View(updateDto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, UpdateCropStatusDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            await _apiService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(dto);
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _apiService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}