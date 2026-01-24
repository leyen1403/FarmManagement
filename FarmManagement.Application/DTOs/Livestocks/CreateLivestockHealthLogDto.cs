namespace FarmManagement.Application.DTOs.Livestocks;

public class CreateLivestockHealthLogDto
{
    public int LivestockId { get; set; }
    public int HealthStatusId { get; set; }
    public DateTime CheckDate { get; set; }
    public string? Symptom { get; set; }
    public string? Treatment { get; set; }
    public decimal MedicineCost { get; set; }
    public string? VetName { get; set; }
}
