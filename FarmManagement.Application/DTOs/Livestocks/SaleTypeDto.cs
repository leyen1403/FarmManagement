using FarmManagement.Domain.Entities.Livestocks;

namespace FarmManagement.Application.DTOs.Livestocks;

public class SaleTypeDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public SaleMethod SaleMethod { get; set; }

    /// <summary>
    /// Mô tả phương thức bán để hiển thị trên UI
    /// </summary>
    public string SaleMethodDisplay => SaleMethod switch
    {
        SaleMethod.PerHead => "Theo con",
        SaleMethod.PerKilogram => "Theo kg",
        SaleMethod.PerLot => "Theo lô",
        SaleMethod.PerSet => "Theo bộ",
        _ => "-"
    };
}
