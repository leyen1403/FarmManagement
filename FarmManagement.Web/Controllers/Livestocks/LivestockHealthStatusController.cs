using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Web.Services.Livestocks;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Web.Controllers.Livestocks;

public class LivestockHealthStatusController : Controller
{
    private readonly LivestockHealthStatusApiService _service;

    public LivestockHealthStatusController(LivestockHealthStatusApiService service)
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
        return View(new CreateLivestockHealthStatusDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateLivestockHealthStatusDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            await _service.CreateAsync(dto);
            TempData["Success"] = "Thêm trạng thái sức khỏe thành công!";
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

        var dto = new UpdateLivestockHealthStatusDto
        {
            Id = item.Id,
            Code = item.Code,
            Name = item.Name
        };

        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateLivestockHealthStatusDto dto)
    {
        if (id != dto.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            await _service.UpdateAsync(id, dto);
            TempData["Success"] = "Cập nhật trạng thái sức khỏe thành công!";
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
            TempData["Success"] = "Xóa trạng thái sức khỏe thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}
