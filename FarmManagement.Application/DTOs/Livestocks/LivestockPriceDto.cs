using FarmManagement.Domain.Entities.Livestocks;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Application.DTOs.Livestocks;

public class LivestockPriceDto
{
    public int Id { get; set; }
    public int LivestockId { get; set; }
    public GenderType Gender { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập đơn giá")]
    [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn hoặc bằng0")]
    public decimal UnitPrice { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn ngày bắt đầu")]
    public DateTime EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Note { get; set; }
    public DateTime CreatedDate { get; set; }

    public string EffectiveDisplay => EffectiveTo.HasValue
    ? $"{EffectiveFrom:dd/MM/yyyy} - {EffectiveTo:dd/MM/yyyy}"
    : $"{EffectiveFrom:dd/MM/yyyy} - Vô thời hạn";
}
