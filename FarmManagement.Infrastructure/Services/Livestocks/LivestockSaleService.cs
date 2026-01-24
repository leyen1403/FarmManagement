using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Exceptions;
using FarmManagement.Application.Interfaces.Livestocks;
using FarmManagement.Application.Interfaces.Common;
using FarmManagement.Domain.Entities.Livestocks;
using FarmManagement.Domain.Entities.Common;
using FarmManagement.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.Infrastructure.Services.Livestocks;

public class LivestockSaleService : ILivestockSaleService
{
    private readonly FarmManagementDbContext _context;
    private readonly IActivityLogService _activityLogService;
    private readonly ILivestockPriceService _priceService;

    public LivestockSaleService(FarmManagementDbContext context, IActivityLogService activityLogService, ILivestockPriceService priceService)
    {
        _context = context;
        _activityLogService = activityLogService;
        _priceService = priceService;
    }

    public async Task<List<LivestockSaleDto>> GetByLivestockIdAsync(int livestockId)
    {
        var items = await _context.LivestockSales
        .Include(x => x.SaleType)
        .Include(x => x.Details)
        .Where(x => x.LivestockId == livestockId)
        .OrderByDescending(x => x.SaleDate)
        .ThenByDescending(x => x.Id)
        .ToListAsync();

        return items.Select(MapToDto).ToList();
    }

    public async Task<LivestockSaleDto?> GetByIdAsync(int id)
    {
        var entity = await _context.LivestockSales
        .Include(x => x.SaleType)
        .Include(x => x.Details.OrderBy(d => d.LineNumber))
        .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return null;

        return MapToDto(entity);
    }

    public async Task<int> CreateAsync(CreateLivestockSaleDto dto)
    {
        if (!await _context.Livestocks.AnyAsync(x => x.Id == dto.LivestockId && !x.IsDeleted))
            throw new BusinessException("Vật nuôi không tồn tại");

        var saleType = await _context.SaleTypes.FirstOrDefaultAsync(x => x.Id == dto.SaleTypeId)
        ?? throw new BusinessException("Loại bán không tồn tại");

        if (!dto.Details.Any())
            throw new BusinessException("Đơn hàng phải có ít nhất một chi tiết");

        // Generate order code
        var orderCode = await GenerateOrderCodeAsync();

        var entity = new LivestockSale
        {
            OrderCode = orderCode,
            LivestockId = dto.LivestockId,
            SaleTypeId = dto.SaleTypeId,
            SaleDate = dto.SaleDate,
            Buyer = dto.Buyer,
            BuyerPhone = dto.BuyerPhone,
            Note = dto.Note,
            Status = OrderStatus.Completed,
            CreatedDate = DateTime.UtcNow
        };

        // Add details
        int lineNumber = 1;
        foreach (var detailDto in dto.Details)
        {
            decimal unitPrice = detailDto.UnitPrice;

            // If selling by kg and unit price not provided, try to lookup active price
            if (saleType.SaleMethod == SaleMethod.PerKilogram && unitPrice <= 0)
            {
                var priceDto = await _priceService.GetActivePriceAsync(dto.LivestockId, detailDto.Gender, dto.SaleDate);
                if (priceDto != null)
                {
                    unitPrice = priceDto.UnitPrice;
                }
            }

            // Validate detail after resolving unit price
            ValidateDetail(saleType.SaleMethod, detailDto.Quantity, detailDto.Weight, unitPrice);

            var amount = CalculateAmount(saleType.SaleMethod, detailDto.Quantity, detailDto.Weight, unitPrice);
            entity.Details.Add(new LivestockSaleDetail
            {
                Gender = detailDto.Gender,
                Quantity = detailDto.Quantity,
                Weight = detailDto.Weight,
                UnitPrice = unitPrice,
                Amount = amount,
                Note = detailDto.Note,
                LineNumber = lineNumber++
            });
        }

        // Calculate totals
        entity.TotalQuantity = entity.Details.Sum(d => d.Quantity);
        entity.TotalWeight = entity.Details.Sum(d => d.Weight);
        entity.TotalAmount = entity.Details.Sum(d => d.Amount);

        _context.LivestockSales.Add(entity);
        await _context.SaveChangesAsync();

        var livestock = await _context.Livestocks.FirstOrDefaultAsync(x => x.Id == dto.LivestockId);
        _activityLogService.LogActivity(
        ActivityActionTypes.Create,
        nameof(LivestockSale),
        entity.Id,
        livestock?.TagCode ?? $"Livestock-{dto.LivestockId}",
        $"Thêm đơn hàng bán {orderCode} - {entity.Details.Count} dòng - Tổng: {entity.TotalAmount:N0}đ"
        );

        return entity.Id;
    }

    public async Task UpdateAsync(int id, UpdateLivestockSaleDto dto)
    {
        var entity = await _context.LivestockSales
        .Include(x => x.Details)
        .FirstOrDefaultAsync(x => x.Id == id)
        ?? throw new NotFoundException("Đơn hàng không tồn tại");

        var saleType = await _context.SaleTypes.FirstOrDefaultAsync(x => x.Id == dto.SaleTypeId)
        ?? throw new BusinessException("Loại bán không tồn tại");

        if (!dto.Details.Any())
            throw new BusinessException("Đơn hàng phải có ít nhất một chi tiết");

        entity.SaleTypeId = dto.SaleTypeId;
        entity.SaleDate = dto.SaleDate;
        entity.Buyer = dto.Buyer;
        entity.BuyerPhone = dto.BuyerPhone;
        entity.Note = dto.Note;

        // Clear existing details and add new ones
        _context.Set<LivestockSaleDetail>().RemoveRange(entity.Details);
        entity.Details.Clear();

        int lineNumber = 1;
        foreach (var detailDto in dto.Details)
        {
            decimal unitPrice = detailDto.UnitPrice;
            if (saleType.SaleMethod == SaleMethod.PerKilogram && unitPrice <= 0)
            {
                var priceDto = await _priceService.GetActivePriceAsync(entity.LivestockId, detailDto.Gender, dto.SaleDate);
                if (priceDto != null)
                {
                    unitPrice = priceDto.UnitPrice;
                }
            }

            ValidateDetail(saleType.SaleMethod, detailDto.Quantity, detailDto.Weight, unitPrice);

            var amount = CalculateAmount(saleType.SaleMethod, detailDto.Quantity, detailDto.Weight, unitPrice);
            entity.Details.Add(new LivestockSaleDetail
            {
                Gender = detailDto.Gender,
                Quantity = detailDto.Quantity,
                Weight = detailDto.Weight,
                UnitPrice = unitPrice,
                Amount = amount,
                Note = detailDto.Note,
                LineNumber = lineNumber++
            });
        }

        // Recalculate totals
        entity.TotalQuantity = entity.Details.Sum(d => d.Quantity);
        entity.TotalWeight = entity.Details.Sum(d => d.Weight);
        entity.TotalAmount = entity.Details.Sum(d => d.Amount);

        await _context.SaveChangesAsync();

        var livestock = await _context.Livestocks.FirstOrDefaultAsync(x => x.Id == entity.LivestockId);
        _activityLogService.LogActivity(
        ActivityActionTypes.Update,
        nameof(LivestockSale),
        entity.Id,
        livestock?.TagCode ?? $"Livestock-{entity.LivestockId}",
        $"Cập nhật đơn hàng {entity.OrderCode} - {entity.Details.Count} dòng - Tổng: {entity.TotalAmount:N0}đ"
        );
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.LivestockSales
         .Include(x => x.Details)
         .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return;

        var livestockId = entity.LivestockId;
        var orderCode = entity.OrderCode;
        var totalAmount = entity.TotalAmount;
        var livestock = await _context.Livestocks.FirstOrDefaultAsync(x => x.Id == livestockId);

        // Remove details first
        _context.Set<LivestockSaleDetail>().RemoveRange(entity.Details);
        _context.LivestockSales.Remove(entity);
        await _context.SaveChangesAsync();

        _activityLogService.LogActivity(
        ActivityActionTypes.Delete,
        nameof(LivestockSale),
        id,
        livestock?.TagCode ?? $"Livestock-{livestockId}",
        $"Xóa đơn hàng {orderCode} - Tổng: {totalAmount:N0}đ"
        );
    }

    public async Task<int> AddDetailAsync(int saleId, CreateLivestockSaleDetailDto dto)
    {
        var sale = await _context.LivestockSales
        .Include(x => x.SaleType)
        .Include(x => x.Details)
        .FirstOrDefaultAsync(x => x.Id == saleId)
        ?? throw new NotFoundException("Đơn hàng không tồn tại");

        decimal unitPrice = dto.UnitPrice;
        if (sale.SaleType.SaleMethod == SaleMethod.PerKilogram && unitPrice <= 0)
        {
            var priceDto = await _priceService.GetActivePriceAsync(sale.LivestockId, dto.Gender, sale.SaleDate);
            if (priceDto != null)
                unitPrice = priceDto.UnitPrice;
        }

        ValidateDetail(sale.SaleType.SaleMethod, dto.Quantity, dto.Weight, unitPrice);

        var amount = CalculateAmount(sale.SaleType.SaleMethod, dto.Quantity, dto.Weight, unitPrice);
        var maxLineNumber = sale.Details.Any() ? sale.Details.Max(d => d.LineNumber) : 0;

        var detail = new LivestockSaleDetail
        {
            LivestockSaleId = saleId,
            Gender = dto.Gender,
            Quantity = dto.Quantity,
            Weight = dto.Weight,
            UnitPrice = unitPrice,
            Amount = amount,
            Note = dto.Note,
            LineNumber = maxLineNumber + 1
        };

        _context.Set<LivestockSaleDetail>().Add(detail);

        // Recalculate totals
        sale.TotalQuantity += dto.Quantity;
        sale.TotalWeight += dto.Weight;
        sale.TotalAmount += amount;

        await _context.SaveChangesAsync();

        return detail.Id;
    }

    public async Task UpdateDetailAsync(int detailId, UpdateLivestockSaleDetailDto dto)
    {
        var detail = await _context.Set<LivestockSaleDetail>()
        .Include(x => x.LivestockSale)
        .ThenInclude(x => x.SaleType)
        .FirstOrDefaultAsync(x => x.Id == detailId)
        ?? throw new NotFoundException("Chi tiết đơn hàng không tồn tại");

        var sale = detail.LivestockSale;

        decimal unitPrice = dto.UnitPrice;
        if (sale.SaleType.SaleMethod == SaleMethod.PerKilogram && unitPrice <= 0)
        {
            var priceDto = await _priceService.GetActivePriceAsync(sale.LivestockId, dto.Gender, sale.SaleDate);
            if (priceDto != null)
                unitPrice = priceDto.UnitPrice;
        }

        ValidateDetail(sale.SaleType.SaleMethod, dto.Quantity, dto.Weight, unitPrice);

        // Calculate difference for totals
        var oldAmount = detail.Amount;
        var oldQuantity = detail.Quantity;
        var oldWeight = detail.Weight;

        var newAmount = CalculateAmount(sale.SaleType.SaleMethod, dto.Quantity, dto.Weight, unitPrice);

        detail.Gender = dto.Gender;
        detail.Quantity = dto.Quantity;
        detail.Weight = dto.Weight;
        detail.UnitPrice = unitPrice;
        detail.Amount = newAmount;
        detail.Note = dto.Note;

        // Update sale totals
        sale.TotalQuantity = sale.TotalQuantity - oldQuantity + dto.Quantity;
        sale.TotalWeight = sale.TotalWeight - oldWeight + dto.Weight;
        sale.TotalAmount = sale.TotalAmount - oldAmount + newAmount;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteDetailAsync(int detailId)
    {
        var detail = await _context.Set<LivestockSaleDetail>()
        .Include(x => x.LivestockSale)
        .FirstOrDefaultAsync(x => x.Id == detailId);

        if (detail == null)
            return;

        var sale = detail.LivestockSale;

        // Update sale totals
        sale.TotalQuantity -= detail.Quantity;
        sale.TotalWeight -= detail.Weight;
        sale.TotalAmount -= detail.Amount;

        _context.Set<LivestockSaleDetail>().Remove(detail);
        await _context.SaveChangesAsync();
    }

    public async Task RecalculateTotalsAsync(int saleId)
    {
        var sale = await _context.LivestockSales
       .Include(x => x.Details)
        .FirstOrDefaultAsync(x => x.Id == saleId);

        if (sale == null)
            return;

        sale.TotalQuantity = sale.Details.Sum(d => d.Quantity);
        sale.TotalWeight = sale.Details.Sum(d => d.Weight);
        sale.TotalAmount = sale.Details.Sum(d => d.Amount);

        await _context.SaveChangesAsync();
    }

    #region Private Methods

    private LivestockSaleDto MapToDto(LivestockSale entity)
    {
        return new LivestockSaleDto
        {
            Id = entity.Id,
            OrderCode = entity.OrderCode,
            LivestockId = entity.LivestockId,
            SaleTypeId = entity.SaleTypeId,
            SaleTypeName = entity.SaleType?.Name,
            SaleMethod = entity.SaleType?.SaleMethod ?? SaleMethod.PerKilogram,
            SaleDate = entity.SaleDate,
            TotalQuantity = entity.TotalQuantity,
            TotalWeight = entity.TotalWeight,
            TotalAmount = entity.TotalAmount,
            Buyer = entity.Buyer,
            BuyerPhone = entity.BuyerPhone,
            Note = entity.Note,
            Status = entity.Status,
            CreatedDate = entity.CreatedDate,
            Details = entity.Details.OrderBy(d => d.LineNumber).Select(d => new LivestockSaleDetailDto
            {
                Id = d.Id,
                LivestockSaleId = d.LivestockSaleId,
                Gender = d.Gender,
                Quantity = d.Quantity,
                Weight = d.Weight,
                UnitPrice = d.UnitPrice,
                Amount = d.Amount,
                Note = d.Note,
                LineNumber = d.LineNumber
            }).ToList()
        };
    }

    private async Task<string> GenerateOrderCodeAsync()
    {
        var today = DateTime.Today;
        var prefix = $"DH{today:yyMMdd}";

        var lastOrder = await _context.LivestockSales
        .Where(x => x.OrderCode != null && x.OrderCode.StartsWith(prefix))
        .OrderByDescending(x => x.OrderCode)
        .FirstOrDefaultAsync();

        int sequence = 1;
        if (lastOrder?.OrderCode != null)
        {
            var lastSequence = lastOrder.OrderCode.Substring(prefix.Length);
            if (int.TryParse(lastSequence, out int parsed))
            {
                sequence = parsed + 1;
            }
        }

        return $"{prefix}{sequence:D3}";
    }

    private static decimal CalculateAmount(SaleMethod saleMethod, int quantity, decimal weight, decimal unitPrice)
    {
        return saleMethod switch
        {
            SaleMethod.PerHead => quantity * unitPrice,
            SaleMethod.PerKilogram => weight * unitPrice,
            SaleMethod.PerLot => unitPrice,
            SaleMethod.PerSet => quantity * unitPrice,
            _ => weight * unitPrice
        };
    }

    private static void ValidateDetail(SaleMethod saleMethod, int quantity, decimal weight, decimal unitPrice)
    {
        switch (saleMethod)
        {
            case SaleMethod.PerHead:
            if (quantity <= 0)
                throw new BusinessException("Số lượng con phải lớn hơn 0");
            if (unitPrice <= 0)
                throw new BusinessException("Đơn giá/con phải lớn hơn 0");
            break;

            case SaleMethod.PerKilogram:
            if (weight <= 0)
                throw new BusinessException("Trọng lượng phải lớn hơn 0");
            if (unitPrice <= 0)
                throw new BusinessException("Đơn giá/kg phải lớn hơn 0");
            break;

            case SaleMethod.PerLot:
            if (unitPrice <= 0)
                throw new BusinessException("Giá trọn lô phải lớn hơn 0");
            break;

            case SaleMethod.PerSet:
            if (quantity <= 0)
                throw new BusinessException("Số bộ/combo phải lớn hơn 0");
            if (unitPrice <= 0)
                throw new BusinessException("Đơn giá/bộ phải lớn hơn 0");
            break;
        }
    }

    #endregion
}
