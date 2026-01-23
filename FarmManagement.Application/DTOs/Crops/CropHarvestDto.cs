using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Application.DTOs.Crops;

/// <summary>
/// DTO đại diện cho thông tin thu hoạch cây trồng.
/// </summary>
public class CropHarvestDto
{
    public int Id { get; set; }
    public int CropId { get; set; }
    public string? CropName { get; set; }
    public DateTime HarvestDate { get; set; }
    public decimal Quantity { get; set; }
    public string? Unit { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Buyer { get; set; }
    public string? Note { get; set; }
    public DateTime CreatedDate { get; set; }
}

/// <summary>
/// DTO dùng để tạo mới thông tin thu hoạch.
/// </summary>
public class CreateCropHarvestDto
{
    [Required(ErrorMessage = "Cây trồng không được để trống")]
    public int CropId { get; set; }

    [Required(ErrorMessage = "Ngày thu hoạch không được để trống")]
    public DateTime HarvestDate { get; set; }

    [Required(ErrorMessage = "Số lượng không được để trống")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
    public decimal Quantity { get; set; }

    public string? Unit { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Giá đơn vị phải >= 0")]
    public decimal UnitPrice { get; set; }

    public string? Buyer { get; set; }
    public string? Note { get; set; }
}

/// <summary>
/// DTO dùng để cập nhật thông tin thu hoạch.
/// </summary>
public class UpdateCropHarvestDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Cây trồng không được để trống")]
    public int CropId { get; set; }

    [Required(ErrorMessage = "Ngày thu hoạch không được để trống")]
    public DateTime HarvestDate { get; set; }

    [Required(ErrorMessage = "Số lượng không được để trống")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
    public decimal Quantity { get; set; }

    public string? Unit { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Giá đơn vị phải >= 0")]
    public decimal UnitPrice { get; set; }

    public string? Buyer { get; set; }
    public string? Note { get; set; }
}
