namespace FarmManagement.Application.DTOs.Livestocks;

public class UpdateLivestockCareLogDto
{
    public int Id { get; set; }
    public int LivestockCareTypeId { get; set; }
    public DateTime CareDate { get; set; }
    public decimal Quantity { get; set; }
    public string? Unit { get; set; }
    public decimal Cost { get; set; }
    public string? Note { get; set; }
}
