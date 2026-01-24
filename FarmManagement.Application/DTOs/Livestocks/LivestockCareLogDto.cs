namespace FarmManagement.Application.DTOs.Livestocks;

public class LivestockCareLogDto
{
    public int Id { get; set; }
    public int LivestockId { get; set; }
    public int LivestockCareTypeId { get; set; }
    public string? LivestockCareTypeName { get; set; }
    public DateTime CareDate { get; set; }
    public decimal Quantity { get; set; }
    public string? Unit { get; set; }
    public decimal Cost { get; set; }
    public string? Note { get; set; }
}
