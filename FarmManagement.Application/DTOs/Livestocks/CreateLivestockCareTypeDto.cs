namespace FarmManagement.Application.DTOs.Livestocks;

public class CreateLivestockCareTypeDto
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
