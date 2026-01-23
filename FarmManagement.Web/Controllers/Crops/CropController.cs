using FarmManagement.Application.DTOs.Crops;
using FarmManagement.Web.Services.Crops;
using FarmManagement.Web.Services.Locations;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Web.Controllers.Crops;

public class CropController : Controller
{
    private readonly CropApiService _cropService;
    private readonly CropTypeApiService _cropTypeService;
    private readonly CropStatusApiService _cropStatusService;
    private readonly LocationApiClient _locationService;
    private readonly CropPriceApiService _cropPriceService;
    private readonly CropHarvestApiService _cropHarvestService;
    private readonly CropCostApiService _cropCostService;
    private readonly CropCareLogApiService _cropCareLogService;
    private readonly CostTypeApiService _costTypeService;
    private readonly CropCareTypeApiService _cropCareTypeService;

    public CropController(
        CropApiService cropService,
        CropTypeApiService cropTypeService,
        CropStatusApiService cropStatusService,
        LocationApiClient locationService,
        CropPriceApiService cropPriceService,
        CropHarvestApiService cropHarvestService,
        CropCostApiService cropCostService,
        CropCareLogApiService cropCareLogService,
        CostTypeApiService costTypeService,
        CropCareTypeApiService cropCareTypeService)
    {
        _cropService = cropService;
        _cropTypeService = cropTypeService;
        _cropStatusService = cropStatusService;
        _locationService = locationService;
        _cropPriceService = cropPriceService;
        _cropHarvestService = cropHarvestService;
        _cropCostService = cropCostService;
        _cropCareLogService = cropCareLogService;
        _costTypeService = costTypeService;
        _cropCareTypeService = cropCareTypeService;
    }

    public async Task<IActionResult> Index(int? cropTypeId = null, int? cropStatusId = null, int? locationId = null)
    {
        var crops = await _cropService.GetAllAsync(cropTypeId, cropStatusId, locationId);

        ViewBag.CropTypes = await _cropTypeService.GetAllAsync(false);
        ViewBag.CropStatuses = await _cropStatusService.GetAllAsync(false);
        // Lấy tất cả Location để lọc (bao gồm cả không hoạt động)
        ViewBag.Locations = await _locationService.GetAllAsync(activeOnly: false);

        ViewBag.SelectedCropTypeId = cropTypeId;
        ViewBag.SelectedCropStatusId = cropStatusId;
        ViewBag.SelectedLocationId = locationId;

        return View(crops);
    }

    public async Task<IActionResult> Details(int id)
    {
        var crop = await _cropService.GetByIdAsync(id);
        if (crop == null)
            return NotFound();

        // Load all related data for this crop
        ViewBag.PriceHistory = await _cropPriceService.GetPriceHistoryAsync(id);
        ViewBag.Harvests = await _cropHarvestService.GetByCropIdAsync(id);
        ViewBag.Costs = await _cropCostService.GetByCropIdAsync(id);
        ViewBag.CareLogs = await _cropCareLogService.GetByCropIdAsync(id);

        // Load dropdowns for modals
        ViewBag.CostTypes = await _cropCostService.GetCostTypesAsync();
        ViewBag.CareTypes = await _cropCareLogService.GetCareTypesAsync();

        return View(crop);
    }

    public async Task<IActionResult> Create()
    {
        await LoadDropdownsAsync();
        return View(new CreateCropDto { PlantDate = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCropDto dto)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdownsAsync();
            return View(dto);
        }

        try
        {
            var id = await _cropService.CreateAsync(dto);
            TempData["Success"] = "Thêm cây trồng thành công!";
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
        var crop = await _cropService.GetByIdAsync(id);
        if (crop == null)
            return NotFound();

        var dto = new UpdateCropDto
        {
            Id = crop.Id,
            Name = crop.Name,
            CropTypeId = crop.CropTypeId,
            LocationId = crop.LocationId,
            CropStatusId = crop.CropStatusId,
            PlantDate = crop.PlantDate,
            ExpectedHarvestDate = crop.ExpectedHarvestDate,
            ActualHarvestDate = crop.ActualHarvestDate,
            EstimatedYield = crop.EstimatedYield,
            Unit = crop.Unit,
            Note = crop.Note
        };

        await LoadDropdownsAsync();
        ViewBag.CropDto = crop;
        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateCropDto dto)
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
            await _cropService.UpdateAsync(id, dto);
            TempData["Success"] = "Cập nhật cây trồng thành công!";
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
            await _cropService.DeleteAsync(id);
            TempData["Success"] = "Xóa cây trồng thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    #region Crop Price Actions

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePrice(CreateCropPriceDto dto)
    {
        try
        {
            await _cropPriceService.CreateAsync(dto);
            TempData["Success"] = "Thêm giá cây trồng thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = dto.CropId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdatePrice(UpdateCropPriceDto dto)
    {
        try
        {
            await _cropPriceService.UpdateAsync(dto.Id, dto);
            TempData["Success"] = "Cập nhật giá thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = dto.CropId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePrice(int id, int cropId)
    {
        try
        {
            await _cropPriceService.DeleteAsync(id);
            TempData["Success"] = "Xóa giá thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = cropId });
    }

    #endregion

    #region Crop Harvest Actions

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateHarvest(CreateCropHarvestDto dto)
    {
        try
        {
            await _cropHarvestService.CreateAsync(dto);
            TempData["Success"] = "Thêm thông tin thu hoạch thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = dto.CropId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateHarvest(UpdateCropHarvestDto dto)
    {
        try
        {
            await _cropHarvestService.UpdateAsync(dto.Id, dto);
            TempData["Success"] = "Cập nhật thông tin thu hoạch thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = dto.CropId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteHarvest(int id, int cropId)
    {
        try
        {
            await _cropHarvestService.DeleteAsync(id);
            TempData["Success"] = "Xóa thông tin thu hoạch thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = cropId });
    }

    #endregion

    #region Crop Cost Actions

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCost(CreateCropCostDto dto)
    {
        try
        {
            await _cropCostService.CreateAsync(dto);
            TempData["Success"] = "Thêm chi phí thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = dto.CropId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateCost(UpdateCropCostDto dto)
    {
        try
        {
            await _cropCostService.UpdateAsync(dto.Id, dto);
            TempData["Success"] = "Cập nhật chi phí thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = dto.CropId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCost(int id, int cropId)
    {
        try
        {
            await _cropCostService.DeleteAsync(id);
            TempData["Success"] = "Xóa chi phí thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = cropId });
    }

    #endregion

    #region Crop Care Log Actions

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCareLog(CreateCropCareLogDto dto)
    {
        try
        {
            await _cropCareLogService.CreateAsync(dto);
            TempData["Success"] = "Thêm nhật ký chăm sóc thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = dto.CropId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateCareLog(UpdateCropCareLogDto dto)
    {
        try
        {
            await _cropCareLogService.UpdateAsync(dto.Id, dto);
            TempData["Success"] = "Cập nhật nhật ký chăm sóc thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = dto.CropId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCareLog(int id, int cropId)
    {
        try
        {
            await _cropCareLogService.DeleteAsync(id);
            TempData["Success"] = "Xóa nhật ký chăm sóc thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = cropId });
    }

    #endregion

    #region CostType and CropCareType Quick Create Actions

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCostType(CreateCostTypeDto dto, int cropId)
    {
        try
        {
            await _costTypeService.CreateAsync(dto);
            TempData["Success"] = "Thêm loại chi phí mới thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = cropId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCropCareType(CreateCropCareTypeDto dto, int cropId)
    {
        try
        {
            await _cropCareTypeService.CreateAsync(dto);
            TempData["Success"] = "Thêm loại chăm sóc mới thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id = cropId });
    }

    #endregion

    private async Task LoadDropdownsAsync()
    {
        ViewBag.CropTypes = await _cropTypeService.GetAllAsync(false);
        ViewBag.CropStatuses = await _cropStatusService.GetAllAsync(false);
        // Chỉ lấy các Location đang hoạt động để chọn khi tạo/sửa cây trồng
        ViewBag.Locations = await _locationService.GetAllAsync(activeOnly: true);
    }
}
