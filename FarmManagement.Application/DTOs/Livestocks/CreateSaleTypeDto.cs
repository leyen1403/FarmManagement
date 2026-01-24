using FarmManagement.Domain.Entities.Livestocks;

namespace FarmManagement.Application.DTOs.Livestocks;

public class CreateSaleTypeDto
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public SaleMethod SaleMethod { get; set; } = SaleMethod.PerKilogram;
}
