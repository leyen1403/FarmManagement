using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Web.Services.Crops;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Web.Controllers.Crops;

public class CropCareTypeController : Controller
{
    private readonly CropCareTypeApiService _service;

    public CropCareTypeController(CropCareTypeApiService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    => View(await _service.GetAllAsync());

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCropCareTypeDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            await _service.CreateAsync(dto);
            TempData["Success"] = "Thêm loại chăm sóc thành công!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return View(dto);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var dto = await _service.GetByIdAsync(id);
        if (dto == null)
            return NotFound();

        var updateDto = new UpdateCropCareTypeDto
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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateCropCareTypeDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            await _service.UpdateAsync(id, dto);
            TempData["Success"] = "Cập nhật loại chăm sóc thành công!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return View(dto);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            TempData["Success"] = "Xóa loại chăm sóc thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActive(int id)
    {
        try
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            var updateDto = new UpdateCropCareTypeDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = !entity.IsActive
            };

            await _service.UpdateAsync(id, updateDto);
            TempData["Success"] = entity.IsActive ? "Đã tắt loại chăm sóc!" : "Đã bật loại chăm sóc!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}
