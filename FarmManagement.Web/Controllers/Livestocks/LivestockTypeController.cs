using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Web.Services.Livestocks;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Web.Controllers.Livestocks;

public class LivestockTypeController : Controller
{
    private readonly LivestockTypeApiService _service;

    public LivestockTypeController(LivestockTypeApiService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var items = await _service.GetAllAsync(true);
        return View(items);
    }

    public IActionResult Create()
    {
        return View(new CreateLivestockTypeDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateLivestockTypeDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            await _service.CreateAsync(dto);
            TempData["Success"] = "Thêm loại vật nuôi thành công!";
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
        var item = await _service.GetByIdAsync(id);
        if (item == null)
            return NotFound();

        var dto = new UpdateLivestockTypeDto
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            IsActive = item.IsActive
        };

        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateLivestockTypeDto dto)
    {
        if (id != dto.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            await _service.UpdateAsync(id, dto);
            TempData["Success"] = "Cập nhật loại vật nuôi thành công!";
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
            TempData["Success"] = "Xóa loại vật nuôi thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}
