// ***********************************************************************
// File: CropDto.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa các DTO liên quan đến cây trồng (Crop)
// ***********************************************************************

using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Application.DTOs.Crops;

/// <summary>
/// DTO đại diện cho cây trồng để hiển thị dữ liệu.
/// </summary>
public class CropDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CropTypeId { get; set; }
    public string? CropTypeName { get; set; }
    public int LocationId { get; set; }
    public string? LocationName { get; set; }
    public int CropStatusId { get; set; }
    public string? CropStatusName { get; set; }
    public DateTime PlantDate { get; set; }
    public DateTime? ExpectedHarvestDate { get; set; }
    public DateTime? ActualHarvestDate { get; set; }
    public decimal? EstimatedYield { get; set; }
    public string? Unit { get; set; }
    public string? Note { get; set; }
    public decimal? CurrentPrice { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

/// <summary>
/// DTO dùng để tạo mới cây trồng.
/// </summary>
public class CreateCropDto
{
    [Required(ErrorMessage = "Tên cây trồng không được để trống")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Loại cây trồng không được để trống")]
    public int CropTypeId { get; set; }

    [Required(ErrorMessage = "Vị trí không được để trống")]
    public int LocationId { get; set; }

    [Required(ErrorMessage = "Trạng thái không được để trống")]
    public int CropStatusId { get; set; }

    [Required(ErrorMessage = "Ngày trồng không được để trống")]
    public DateTime PlantDate { get; set; }

    public DateTime? ExpectedHarvestDate { get; set; }
    public decimal? EstimatedYield { get; set; }
    public string? Unit { get; set; }
    public string? Note { get; set; }
}

/// <summary>
/// DTO dùng để cập nhật cây trồng.
/// </summary>
public class UpdateCropDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên cây trồng không được để trống")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Loại cây trồng không được để trống")]
    public int CropTypeId { get; set; }

    [Required(ErrorMessage = "Vị trí không được để trống")]
    public int LocationId { get; set; }

    [Required(ErrorMessage = "Trạng thái không được để trống")]
    public int CropStatusId { get; set; }

    [Required(ErrorMessage = "Ngày trồng không được để trống")]
    public DateTime PlantDate { get; set; }

    public DateTime? ExpectedHarvestDate { get; set; }
    public DateTime? ActualHarvestDate { get; set; }
    public decimal? EstimatedYield { get; set; }
    public string? Unit { get; set; }
    public string? Note { get; set; }
}
