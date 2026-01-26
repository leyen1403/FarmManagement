using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Web.Services.Livestocks;
using FarmManagement.Web.Services.Locations;
using FarmManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FarmManagement.Web.Controllers.Livestocks;

public class LivestockController : Controller
{
    private readonly LivestockApiService _livestockService;
    private readonly LivestockTypeApiService _livestockTypeService;
    private readonly LivestockStatusApiService _livestockStatusService;
    private readonly LocationApiClient _locationService;
    private readonly LivestockCareLogApiService _careLogService;
    private readonly LivestockHealthLogApiService _healthLogService;
    private readonly LivestockSaleApiService _saleService;
    private readonly LivestockPriceApiService _priceService;

    public LivestockController(
        LivestockApiService livestockService,
        LivestockTypeApiService livestockTypeService,
        LivestockStatusApiService livestockStatusService,
        LocationApiClient locationService,
        LivestockCareLogApiService careLogService,
        LivestockHealthLogApiService healthLogService,
        LivestockSaleApiService saleService,
        LivestockPriceApiService priceService)
    {
        _livestockService = livestockService;
        _livestockTypeService = livestockTypeService;
        _livestockStatusService = livestockStatusService;
        _locationService = locationService;
        _careLogService = careLogService;
        _healthLogService = healthLogService;
        _saleService = saleService;
        _priceService = priceService;
    }

    public async Task<IActionResult> Index(int? livestockTypeId = null, int? livestockStatusId = null, int? locationId = null)
    {
        var livestocks = await _livestockService.GetAllAsync(livestockTypeId, livestockStatusId, locationId);

        ViewBag.LivestockTypes = await _livestockTypeService.GetAllAsync(false);
        ViewBag.LivestockStatuses = await _livestockStatusService.GetAllAsync(false);
        ViewBag.Locations = await _locationService.GetAllAsync(activeOnly: false);

        ViewBag.SelectedLivestockTypeId = livestockTypeId;
        ViewBag.SelectedLivestockStatusId = livestockStatusId;
        ViewBag.SelectedLocationId = locationId;

        return View(livestocks);
    }

    public async Task<IActionResult> Details(int id)
    {
        var livestock = await _livestockService.GetByIdAsync(id);
        if (livestock == null)
            return NotFound();

        // Load related data
        var sales = await _saleService.GetByLivestockIdAsync(id);
        ViewBag.CareLogs = await _careLogService.GetByLivestockIdAsync(id);
        ViewBag.HealthLogs = await _healthLogService.GetByLivestockIdAsync(id);
        ViewBag.Sales = sales;

        // Aggregations: sold by date and sold by gender
        var soldByDate = sales
            .SelectMany(s => s.Details.Select(d => new { s.SaleDate, d.Gender, d.Quantity, d.Weight }))
            .GroupBy(x => x.SaleDate.Date)
            .Select(g => new SoldByDateDto
            {
                Date = g.Key,
                TotalQuantity = g.Sum(x => x.Quantity),
                TotalWeight = g.Sum(x => x.Weight),
                MaleQuantity = g.Where(x => x.Gender == FarmManagement.Domain.Entities.Livestocks.GenderType.Male).Sum(x => x.Quantity),
                FemaleQuantity = g.Where(x => x.Gender == FarmManagement.Domain.Entities.Livestocks.GenderType.Female).Sum(x => x.Quantity),
                MixedQuantity = g.Where(x => x.Gender == FarmManagement.Domain.Entities.Livestocks.GenderType.Mixed).Sum(x => x.Quantity)
            })
            .OrderBy(x => x.Date)
            .ToList();

        var soldByGender = new SoldByGenderDto
        {
            Male = soldByDate.Sum(x => x.MaleQuantity),
            Female = soldByDate.Sum(x => x.FemaleQuantity),
            Mixed = soldByDate.Sum(x => x.MixedQuantity)
        };

        ViewBag.SoldByDate = soldByDate;
        ViewBag.SoldByGender = soldByGender;

        // Load dropdowns for modals
        ViewBag.CareTypes = await _careLogService.GetCareTypesAsync();
        ViewBag.HealthStatuses = await _healthLogService.GetHealthStatusesAsync();
        ViewBag.SaleTypes = await _saleService.GetSaleTypesAsync();

        // Load price configs for livestock
        ViewBag.Prices = await _priceService.GetByLivestockIdAsync(id);

        return View(livestock);
    }

    public async Task<IActionResult> Create()
    {
        await LoadDropdownsAsync();
        return View(new CreateLivestockDto
        {
            ImportDate = DateTime.Today,
            Quantity = 1,
            MaleCount = 0,
            FemaleCount = 0
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateLivestockDto dto)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdownsAsync();
            return View(dto);
        }

        try
        {
            var id = await _livestockService.CreateAsync(dto);
            TempData["Success"] = $"Thêm vật nuôi thành công! ({dto.Quantity} con)";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            await LoadDropdownsAsync();
            return View(dto);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var livestock = await _livestockService.GetByIdAsync(id);
        if (livestock == null)
            return NotFound();

        var dto = new UpdateLivestockDto
        {
            Id = livestock.Id,
            LivestockTypeId = livestock.LivestockTypeId,
            LocationId = livestock.LocationId,
            LivestockStatusId = livestock.LivestockStatusId,
            TagCode = livestock.TagCode,
            Name = livestock.Name,
            Quantity = livestock.Quantity,
            MaleCount = livestock.MaleCount,
            FemaleCount = livestock.FemaleCount,
            ImportDate = livestock.ImportDate,
            ImportWeight = livestock.ImportWeight,
            ImportPrice = livestock.ImportPrice,
            Note = livestock.Note
        };

        await LoadDropdownsAsync();
        ViewBag.LivestockDto = livestock;
        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateLivestockDto dto)
    {
        if (id != dto.Id)
            return BadRequest();

        if (!ModelState.IsValid)
        {
            await LoadDropdownsAsync();
            return View(dto);
        }

        try
        {
            await _livestockService.UpdateAsync(id, dto);
            TempData["Success"] = "Cập nhật vật nuôi thành công!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            await LoadDropdownsAsync();
            return View(dto);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _livestockService.DeleteAsync(id);
            TempData["Success"] = "Xóa vật nuôi thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    #region Care Log Actions

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCareLog(CreateLivestockCareLogDto dto)
    {
        try
        {
            await _careLogService.CreateAsync(dto);
            TempData["Success"] = "Thêm nhật ký chăm sóc thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = dto.LivestockId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateCareLog(UpdateLivestockCareLogDto dto, int livestockId)
    {
        try
        {
            await _careLogService.UpdateAsync(dto.Id, dto);
            TempData["Success"] = "Cập nhật nhật ký chăm sóc thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = livestockId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCareLog(int id, int livestockId)
    {
        try
        {
            await _careLogService.DeleteAsync(id);
            TempData["Success"] = "Xóa nhật ký chăm sóc thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = livestockId });
    }

    #endregion

    #region Health Log Actions

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateHealthLog(CreateLivestockHealthLogDto dto)
    {
        try
        {
            await _healthLogService.CreateAsync(dto);
            TempData["Success"] = "Thêm nhật ký sức khỏe thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = dto.LivestockId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateHealthLog(UpdateLivestockHealthLogDto dto, int livestockId)
    {
        try
        {
            await _healthLogService.UpdateAsync(dto.Id, dto);
            TempData["Success"] = "Cập nhật nhật ký sức khỏe thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = livestockId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteHealthLog(int id, int livestockId)
    {
        try
        {
            await _healthLogService.DeleteAsync(id);
            TempData["Success"] = "Xóa nhật ký sức khỏe thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = livestockId });
    }

    #endregion

    #region Sale Actions

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateSale(CreateLivestockSaleDto dto)
    {
        try
        {
            await _saleService.CreateAsync(dto);
            TempData["Success"] = "Thêm giao dịch bán thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = dto.LivestockId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateSale(UpdateLivestockSaleDto dto, int livestockId)
    {
        try
        {
            await _saleService.UpdateAsync(dto.Id, dto);
            TempData["Success"] = "Cập nhật giao dịch bán thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = livestockId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteSale(int id, int livestockId)
    {
        try
        {
            await _saleService.DeleteAsync(id);
            TempData["Success"] = "Xóa giao dịch bán thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = livestockId });
    }

    #endregion

    #region Price Actions

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePrice(LivestockPriceDto dto)
    {
        try
        {
            await _priceService.CreateAsync(dto);
            TempData["Success"] = "Thêm giá thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = dto.LivestockId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdatePrice(LivestockPriceDto dto)
    {
        try
        {
            await _priceService.UpdateAsync(dto.Id, dto);
            TempData["Success"] = "Cập nhật giá thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = dto.LivestockId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePrice(int id, int livestockId)
    {
        try
        {
            await _priceService.DeleteAsync(id);
            TempData["Success"] = "Xóa giá thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = livestockId });
    }

    #endregion

    private async Task LoadDropdownsAsync()
    {
        ViewBag.LivestockTypes = await _livestockTypeService.GetAllAsync(false);
        ViewBag.LivestockStatuses = await _livestockStatusService.GetAllAsync(false);
        ViewBag.Locations = await _locationService.GetAllAsync(activeOnly: true);
    }

    [HttpGet]
    public async Task<IActionResult> GetSalesAggregations(int id)
    {
        var sales = await _saleService.GetByLivestockIdAsync(id);
        var soldByDate = sales
            .SelectMany(s => s.Details.Select(d => new { s.SaleDate, d.Gender, d.Quantity, d.Weight }))
            .GroupBy(x => x.SaleDate.Date)
            .Select(g => new SoldByDateDto
            {
                Date = g.Key,
                TotalQuantity = g.Sum(x => x.Quantity),
                TotalWeight = g.Sum(x => x.Weight),
                MaleQuantity = g.Where(x => x.Gender == FarmManagement.Domain.Entities.Livestocks.GenderType.Male).Sum(x => x.Quantity),
                FemaleQuantity = g.Where(x => x.Gender == FarmManagement.Domain.Entities.Livestocks.GenderType.Female).Sum(x => x.Quantity),
                MixedQuantity = g.Where(x => x.Gender == FarmManagement.Domain.Entities.Livestocks.GenderType.Mixed).Sum(x => x.Quantity)
            })
            .OrderBy(x => x.Date)
            .ToList();

        var soldByGender = new SoldByGenderDto
        {
            Male = soldByDate.Sum(x => x.MaleQuantity),
            Female = soldByDate.Sum(x => x.FemaleQuantity),
            Mixed = soldByDate.Sum(x => x.MixedQuantity)
        };

        return Json(new { soldByDate, soldByGender });
    }
}