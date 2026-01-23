using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Web.Services.Crops;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Web.Controllers.Crops;

public class CropTypeController : Controller
{
    private readonly CropTypeApiService _service;

    public CropTypeController(CropTypeApiService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
        => View(await _service.GetAllAsync());

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateCropTypeDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);
        await _service.CreateAsync(dto);
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Edit(int id)
    {
        var dto = await _service.GetByIdAsync(id); // CropTypeDto
        if (dto == null)
            return NotFound();

        // Map sang UpdateCropTypeDto
        var updateDto = new UpdateCropTypeDto
        {
            Id = dto.Id,
            Code = dto.Code,
            Name = dto.Name,
            Description = dto.Description,
            IsActive = dto.IsActive
        };

        return View(updateDto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, UpdateCropTypeDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);
        await _service.UpdateAsync(id, dto);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> ToggleActive(int id)
    {
        var entity = await _service.GetByIdAsync(id);
        if (entity == null)
            return NotFound();

        var updateDto = new UpdateCropTypeDto
        {
            Id = entity.Id,
            Code = entity.Code,
            Name = entity.Name,
            Description = entity.Description,
            IsActive = !entity.IsActive // đảo trạng thái
        };

        await _service.UpdateAsync(id, updateDto);
        return RedirectToAction(nameof(Index));
    }
}
