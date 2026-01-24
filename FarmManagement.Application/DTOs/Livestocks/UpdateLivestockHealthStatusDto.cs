namespace FarmManagement.Application.DTOs.Livestocks;

public class UpdateLivestockHealthStatusDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
}
