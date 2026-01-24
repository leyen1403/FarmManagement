using FarmManagement.Domain.Entities.Livestocks;

namespace FarmManagement.Application.DTOs.Livestocks;

/// <summary>
/// DTO hiển thị chi tiết dòng bán.
/// </summary>
public class LivestockSaleDetailDto
{
    public int Id { get; set; }
    public int LivestockSaleId { get; set; }
    public GenderType Gender { get; set; }
    public int Quantity { get; set; }
    public decimal Weight { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Amount { get; set; }
    public string? Note { get; set; }
    public int LineNumber { get; set; }

    /// <summary>
    /// Hiển thị giới tính
    /// </summary>
    public string GenderDisplay => Gender switch
    {
        GenderType.Male => "Đực",
        GenderType.Female => "Cái",
        GenderType.Mixed => "Hỗn hợp",
        _ => "-"
    };
}

/// <summary>
/// DTO tạo mới chi tiết dòng bán.
/// </summary>
public class CreateLivestockSaleDetailDto
{
    public GenderType Gender { get; set; }
    public int Quantity { get; set; }
    public decimal Weight { get; set; }
    public decimal UnitPrice { get; set; }
    public string? Note { get; set; }
}

/// <summary>
/// DTO cập nhật chi tiết dòng bán.
/// </summary>
public class UpdateLivestockSaleDetailDto
{
    public int Id { get; set; }
    public GenderType Gender { get; set; }
    public int Quantity { get; set; }
    public decimal Weight { get; set; }
    public decimal UnitPrice { get; set; }
    public string? Note { get; set; }
}
