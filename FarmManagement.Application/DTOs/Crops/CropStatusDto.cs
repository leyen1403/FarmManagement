// ***********************************************************************
// File: CropStatusDto.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa các DTO liên quan đến trạng thái cây trồng (CropStatus) 
// dùng để truyền dữ liệu giữa các tầng trong ứng dụng.
// ***********************************************************************

using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Application.DTOs.Crops
{
    /// <summary>
    /// DTO đại diện cho trạng thái cây trồng (CropStatus) để hiển thị dữ liệu.
    /// </summary>
    public class CropStatusDto
    {
        /// <summary>
        /// ID của trạng thái cây trồng.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Mã trạng thái cây trồng.
        /// </summary>
        public string Code { get; set; } = null!;

        /// <summary>
        /// Tên trạng thái cây trồng.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Mô tả trạng thái cây trồng (nếu có).
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Trạng thái hoạt động.
        /// </summary>
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// DTO dùng để tạo mới trạng thái cây trồng.
    /// </summary>
    public class CreateCropStatusDto
    {
        /// <summary>
        /// Mã trạng thái cây trồng. Bắt buộc.
        /// </summary>
        [Required]
        public string Code { get; set; } = null!;

        /// <summary>
        /// Tên trạng thái cây trồng. Bắt buộc.
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Mô tả trạng thái cây trồng (tùy chọn).
        /// </summary>
        public string? Description { get; set; }
    }

    /// <summary>
    /// DTO dùng để cập nhật trạng thái cây trồng.
    /// </summary>
    public class UpdateCropStatusDto
    {
        /// <summary>
        /// ID của trạng thái cây trồng cần cập nhật.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Mã trạng thái cây trồng. Bắt buộc.
        /// </summary>
        [Required]
        public string Code { get; set; } = null!;

        /// <summary>
        /// Tên trạng thái cây trồng. Bắt buộc.
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Mô tả trạng thái cây trồng (tùy chọn).
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Trạng thái hoạt động.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
