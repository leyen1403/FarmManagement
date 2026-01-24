namespace FarmManagement.Application.DTOs.Livestocks;

public class LivestockDto
{
    public int Id { get; set; }
    public int LivestockTypeId { get; set; }
    public string? LivestockTypeName { get; set; }
    public int LocationId { get; set; }
    public string? LocationName { get; set; }
    public int LivestockStatusId { get; set; }
    public string? LivestockStatusName { get; set; }
    public string? TagCode { get; set; }
    public string? Name { get; set; }

    /// <summary>
    /// Tổng số lượng vật nuôi
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Số lượng con đực
    /// </summary>
    public int MaleCount { get; set; }

    /// <summary>
    /// Số lượng con cái
    /// </summary>
    public int FemaleCount { get; set; }

    public DateTime ImportDate { get; set; }

    /// <summary>
    /// Trọng lượng trung bình khi nhập (kg/con)
    /// </summary>
    public decimal ImportWeight { get; set; }

    /// <summary>
    /// Tổng trọng lượng khi nhập (kg)
    /// </summary>
    public decimal TotalImportWeight { get; set; }

    /// <summary>
    /// Giá nhập đơn vị (VND/con)
    /// </summary>
    public decimal ImportPrice { get; set; }

    /// <summary>
    /// Tổng giá trị nhập (VND)
    /// </summary>
    public decimal TotalImportPrice { get; set; }

    public string? Note { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Hiển thị thông tin số lượng (VD: "10 con (5 đực, 5 cái)")
    /// </summary>
    public string QuantityDisplay => MaleCount > 0 || FemaleCount > 0
   ? $"{Quantity} con ({MaleCount} đực, {FemaleCount} cái)"
        : $"{Quantity} con";
}
