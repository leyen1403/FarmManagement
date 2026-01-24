using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Application.DTOs.Livestocks;

public class CreateLivestockDto
{
    [Required(ErrorMessage = "Vui lòng chọn loại vật nuôi")]
    public int LivestockTypeId { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn vị trí")]
    public int LocationId { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn trạng thái")]
    public int LivestockStatusId { get; set; }

    public string? TagCode { get; set; }

    /// <summary>
    /// Tên/Mô tả đàn vật nuôi
    /// </summary>
    [StringLength(200)]
    public string? Name { get; set; }

    /// <summary>
    /// Tổng số lượng vật nuôi
    /// </summary>
    [Required(ErrorMessage = "Vui lòng nhập số lượng")]
    [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
    public int Quantity { get; set; } = 1;

    /// <summary>
    /// Số lượng con đực
    /// </summary>
    [Range(0, int.MaxValue)]
    public int MaleCount { get; set; }

    /// <summary>
    /// Số lượng con cái
    /// </summary>
    [Range(0, int.MaxValue)]
    public int FemaleCount { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn ngày nhập")]
    public DateTime ImportDate { get; set; } = DateTime.Today;

    /// <summary>
    /// Trọng lượng trung bình khi nhập (kg/con)
    /// </summary>
    [Range(0, double.MaxValue)]
    public decimal ImportWeight { get; set; }

    /// <summary>
    /// Giá nhập đơn vị (VND/con)
    /// </summary>
    [Range(0, double.MaxValue)]
    public decimal ImportPrice { get; set; }

    public string? Note { get; set; }
}
