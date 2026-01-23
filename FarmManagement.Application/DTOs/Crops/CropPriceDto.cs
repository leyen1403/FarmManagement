// ***********************************************************************
// File: CropPriceDto.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa các DTO liên quan đến giá cây trồng (CropPrice)
// ***********************************************************************

using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Application.DTOs.Crops;

/// <summary>
/// DTO đại diện cho giá cây trồng để hiển thị dữ liệu.
/// </summary>
public class CropPriceDto
{
    public int Id { get; set; }
    public int CropId { get; set; }
    public string? CropName { get; set; }
    public decimal Price { get; set; }
    public string Unit { get; set; } = null!;
    public DateTime EffectiveDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? Note { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
}

/// <summary>
/// DTO dùng để tạo mới giá cây trồng.
/// </summary>
public class CreateCropPriceDto
{
    [Required(ErrorMessage = "Cây trồng không được để trống")]
    public int CropId { get; set; }

    [Required(ErrorMessage = "Giá không được để trống")]
    [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Đơn vị không được để trống")]
    public string Unit { get; set; } = null!;

    [Required(ErrorMessage = "Ngày áp dụng không được để trống")]
    public DateTime EffectiveDate { get; set; }

    public DateTime? ExpiryDate { get; set; }
    public string? Note { get; set; }
}

/// <summary>
/// DTO dùng để cập nhật giá cây trồng.
/// </summary>
public class UpdateCropPriceDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Cây trồng không được để trống")]
    public int CropId { get; set; }

    [Required(ErrorMessage = "Giá không được để trống")]
    [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Đơn vị không được để trống")]
    public string Unit { get; set; } = null!;

    [Required(ErrorMessage = "Ngày áp dụng không được để trống")]
    public DateTime EffectiveDate { get; set; }

    public DateTime? ExpiryDate { get; set; }
    public string? Note { get; set; }
    public bool IsActive { get; set; }
}
