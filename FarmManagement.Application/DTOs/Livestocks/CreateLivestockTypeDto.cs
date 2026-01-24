namespace FarmManagement.Application.DTOs.Livestocks;

public class CreateLivestockTypeDto
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
