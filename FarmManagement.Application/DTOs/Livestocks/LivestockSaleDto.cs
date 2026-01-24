using FarmManagement.Domain.Entities.Livestocks;

namespace FarmManagement.Application.DTOs.Livestocks;

/// <summary>
/// DTO hiển thị đơn hàng bán vật nuôi.
/// </summary>
public class LivestockSaleDto
{
    public int Id { get; set; }
    public string? OrderCode { get; set; }
    public int LivestockId { get; set; }
    public int SaleTypeId { get; set; }
    public string? SaleTypeName { get; set; }
    public SaleMethod SaleMethod { get; set; }
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Tổng số lượng con bán
    /// </summary>
    public int TotalQuantity { get; set; }

    /// <summary>
    /// Tổng trọng lượng (kg)
    /// </summary>
    public decimal TotalWeight { get; set; }

    /// <summary>
    /// Tổng tiền đơn hàng
    /// </summary>
    public decimal TotalAmount { get; set; }

    public string? Buyer { get; set; }
    public string? BuyerPhone { get; set; }
    public string? Note { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Danh sách chi tiết đơn hàng
    /// </summary>
    public List<LivestockSaleDetailDto> Details { get; set; } = new();

    /// <summary>
    /// Hiển thị trạng thái đơn hàng
    /// </summary>
    public string StatusDisplay => Status switch
    {
        OrderStatus.Draft => "Nháp",
        OrderStatus.Completed => "Hoàn thành",
        OrderStatus.Cancelled => "Đã hủy",
        _ => "-"
    };

    /// <summary>
    /// Hiển thị số lượng/trọng lượng
    /// </summary>
    public string QuantityDisplay => SaleMethod switch
    {
        SaleMethod.PerHead => $"{TotalQuantity} con",
        SaleMethod.PerKilogram => $"{TotalWeight:N2} kg ({TotalQuantity} con)",
        SaleMethod.PerLot => "Trọn lô",
        SaleMethod.PerSet => $"{TotalQuantity} bộ",
        _ => "-"
    };

    /// <summary>
    /// Số dòng chi tiết trong đơn hàng
    /// </summary>
    public int DetailCount => Details.Count;
}

/// <summary>
/// DTO tạo mới đơn hàng bán vật nuôi.
/// </summary>
public class CreateLivestockSaleDto
{
    public int LivestockId { get; set; }
    public int SaleTypeId { get; set; }
    public DateTime SaleDate { get; set; } = DateTime.Today;
    public string? Buyer { get; set; }
    public string? BuyerPhone { get; set; }
    public string? Note { get; set; }

    /// <summary>
    /// Danh sách chi tiết đơn hàng (các lượt cân)
    /// </summary>
    public List<CreateLivestockSaleDetailDto> Details { get; set; } = new();
}

/// <summary>
/// DTO cập nhật đơn hàng bán vật nuôi.
/// </summary>
public class UpdateLivestockSaleDto
{
    public int Id { get; set; }
    public int SaleTypeId { get; set; }
    public DateTime SaleDate { get; set; }
    public string? Buyer { get; set; }
    public string? BuyerPhone { get; set; }
    public string? Note { get; set; }

    /// <summary>
    /// Danh sách chi tiết đơn hàng (các lượt cân)
    /// </summary>
    public List<UpdateLivestockSaleDetailDto> Details { get; set; } = new();
}
