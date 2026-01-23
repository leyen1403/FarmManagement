using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Application.DTOs.Crops;

/// <summary>
/// DTO đại diện cho nhật ký chăm sóc cây trồng.
/// </summary>
public class CropCareLogDto
{
    public int Id { get; set; }
    public int CropId { get; set; }
    public string? CropName { get; set; }
    public int CropCareTypeId { get; set; }
    public string? CropCareTypeName { get; set; }
    public DateTime CareDate { get; set; }
    public string? Description { get; set; }
    public decimal Cost { get; set; }
    public DateTime CreatedDate { get; set; }
}

/// <summary>
/// DTO dùng để tạo mới nhật ký chăm sóc.
/// </summary>
public class CreateCropCareLogDto
{
    [Required(ErrorMessage = "Cây trồng không được để trống")]
    public int CropId { get; set; }

    [Required(ErrorMessage = "Loại chăm sóc không được để trống")]
    public int CropCareTypeId { get; set; }

    [Required(ErrorMessage = "Ngày chăm sóc không được để trống")]
    public DateTime CareDate { get; set; }

    public string? Description { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Chi phí phải >= 0")]
    public decimal Cost { get; set; }
}

/// <summary>
/// DTO dùng để cập nhật nhật ký chăm sóc.
/// </summary>
public class UpdateCropCareLogDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Cây trồng không được để trống")]
    public int CropId { get; set; }

    [Required(ErrorMessage = "Loại chăm sóc không được để trống")]
    public int CropCareTypeId { get; set; }

    [Required(ErrorMessage = "Ngày chăm sóc không được để trống")]
    public DateTime CareDate { get; set; }

    public string? Description { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Chi phí phải >= 0")]
    public decimal Cost { get; set; }
}

/// <summary>
/// DTO đại diện cho loại chăm sóc cây trồng.
/// </summary>
public class CropCareTypeDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>
/// DTO dùng để tạo mới loại chăm sóc.
/// </summary>
public class CreateCropCareTypeDto
{
    [Required(ErrorMessage = "Mã loại chăm sóc không được để trống")]
    [StringLength(50, ErrorMessage = "Mã loại chăm sóc không quá 50 ký tự")]
    public string Code { get; set; } = null!;

    [Required(ErrorMessage = "Tên loại chăm sóc không được để trống")]
    [StringLength(200, ErrorMessage = "Tên loại chăm sóc không quá 200 ký tự")]
    public string Name { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Mô tả không quá 500 ký tự")]
    public string? Description { get; set; }
}

/// <summary>
/// DTO dùng để cập nhật loại chăm sóc.
/// </summary>
public class UpdateCropCareTypeDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Mã loại chăm sóc không được để trống")]
    [StringLength(50, ErrorMessage = "Mã loại chăm sóc không quá 50 ký tự")]
    public string Code { get; set; } = null!;

    [Required(ErrorMessage = "Tên loại chăm sóc không được để trống")]
    [StringLength(200, ErrorMessage = "Tên loại chăm sóc không quá 200 ký tự")]
    public string Name { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Mô tả không quá 500 ký tự")]
    public string? Description { get; set; }

    public bool IsActive { get; set; }
}
