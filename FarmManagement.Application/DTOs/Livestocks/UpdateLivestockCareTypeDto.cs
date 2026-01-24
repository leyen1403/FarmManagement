namespace FarmManagement.Application.DTOs.Livestocks;

public class UpdateLivestockCareTypeDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
