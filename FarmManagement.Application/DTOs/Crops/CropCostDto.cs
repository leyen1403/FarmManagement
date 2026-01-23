using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Application.DTOs.Crops;

/// <summary>
/// DTO đại diện cho chi phí cây trồng.
/// </summary>
public class CropCostDto
{
    public int Id { get; set; }
    public int CropId { get; set; }
    public string? CropName { get; set; }
    public int CostTypeId { get; set; }
    public string? CostTypeName { get; set; }
    public DateTime CostDate { get; set; }
    public decimal Quantity { get; set; }
    public string? Unit { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Note { get; set; }
    public DateTime CreatedDate { get; set; }
}

/// <summary>
/// DTO dùng để tạo mới chi phí.
/// </summary>
public class CreateCropCostDto
{
    [Required(ErrorMessage = "Cây trồng không được để trống")]
    public int CropId { get; set; }

    [Required(ErrorMessage = "Loại chi phí không được để trống")]
    public int CostTypeId { get; set; }

    [Required(ErrorMessage = "Ngày phát sinh không được để trống")]
    public DateTime CostDate { get; set; }

    [Required(ErrorMessage = "Số lượng không được để trống")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
    public decimal Quantity { get; set; }

    public string? Unit { get; set; }

    [Required(ErrorMessage = "Đơn giá không được để trống")]
    [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải >= 0")]
    public decimal UnitPrice { get; set; }

    public string? Note { get; set; }
}

/// <summary>
/// DTO dùng để cập nhật chi phí.
/// </summary>
public class UpdateCropCostDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Cây trồng không được để trống")]
    public int CropId { get; set; }

    [Required(ErrorMessage = "Loại chi phí không được để trống")]
    public int CostTypeId { get; set; }

    [Required(ErrorMessage = "Ngày phát sinh không được để trống")]
    public DateTime CostDate { get; set; }

    [Required(ErrorMessage = "Số lượng không được để trống")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
    public decimal Quantity { get; set; }

    public string? Unit { get; set; }

    [Required(ErrorMessage = "Đơn giá không được để trống")]
    [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải >= 0")]
    public decimal UnitPrice { get; set; }

    public string? Note { get; set; }
}

/// <summary>
/// DTO đại diện cho loại chi phí.
/// </summary>
public class CostTypeDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>
/// DTO dùng để tạo mới loại chi phí.
/// </summary>
public class CreateCostTypeDto
{
    [Required(ErrorMessage = "Mã loại chi phí không được để trống")]
    [StringLength(50, ErrorMessage = "Mã loại chi phí không quá 50 ký tự")]
    public string Code { get; set; } = null!;

    [Required(ErrorMessage = "Tên loại chi phí không được để trống")]
    [StringLength(200, ErrorMessage = "Tên loại chi phí không quá 200 ký tự")]
    public string Name { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Mô tả không quá 500 ký tự")]
    public string? Description { get; set; }
}

/// <summary>
/// DTO dùng để cập nhật loại chi phí.
/// </summary>
public class UpdateCostTypeDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Mã loại chi phí không được để trống")]
    [StringLength(50, ErrorMessage = "Mã loại chi phí không quá 50 ký tự")]
    public string Code { get; set; } = null!;

    [Required(ErrorMessage = "Tên loại chi phí không được để trống")]
    [StringLength(200, ErrorMessage = "Tên loại chi phí không quá 200 ký tự")]
    public string Name { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Mô tả không quá 500 ký tự")]
    public string? Description { get; set; }

    public bool IsActive { get; set; }
}
