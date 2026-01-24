using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Web.Services.Livestocks;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Web.Controllers.Livestocks;

public class LivestockCareTypeController : Controller
{
    private readonly LivestockCareTypeApiService _service;

    public LivestockCareTypeController(LivestockCareTypeApiService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var items = await _service.GetAllAsync();
        return View(items);
    }

    public IActionResult Create()
    {
        return View(new CreateLivestockCareTypeDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateLivestockCareTypeDto dto)
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
        var item = await _service.GetByIdAsync(id);
        if (item == null)
            return NotFound();

        var dto = new UpdateLivestockCareTypeDto
        {
            Id = item.Id,
            Code = item.Code,
            Name = item.Name
        };

        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateLivestockCareTypeDto dto)
    {
        if (id != dto.Id)
            return BadRequest();

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
}
