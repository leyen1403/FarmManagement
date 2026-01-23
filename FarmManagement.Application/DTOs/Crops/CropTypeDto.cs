// ***********************************************************************
// File: CropTypeDto.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa các DTO liên quan đến loại cây trồng (CropType) 
// dùng để truyền dữ liệu giữa các tầng trong ứng dụng.
// ***********************************************************************

namespace FarmManagement.Application.DTOs.Crops
{
    /// <summary>
    /// DTO đại diện cho loại cây trồng (CropType) để hiển thị dữ liệu.
    /// </summary>
    public class CropTypeDto
    {
        /// <summary>
        /// ID của loại cây trồng.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Mã loại cây trồng.
        /// </summary>
        public string Code { get; set; } = null!;

        /// <summary>
        /// Tên loại cây trồng.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Mô tả loại cây trồng (tùy chọn).
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Trạng thái hoạt động.
        /// </summary>
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// DTO dùng để tạo mới loại cây trồng.
    /// </summary>
    public class CreateCropTypeDto
    {
        /// <summary>
        /// Mã loại cây trồng (tùy chọn).
        /// </summary>
        public string? Code { get; set; } = null!;

        /// <summary>
        /// Tên loại cây trồng. Bắt buộc.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Mô tả loại cây trồng (tùy chọn).
        /// </summary>
        public string? Description { get; set; }
    }

    /// <summary>
    /// DTO dùng để cập nhật loại cây trồng.
    /// </summary>
    public class UpdateCropTypeDto
    {
        /// <summary>
        /// ID của loại cây trồng cần cập nhật.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Mã loại cây trồng (tùy chọn, không bắt buộc thay đổi).
        /// </summary>
        public string? Code { get; set; } = null!;

        /// <summary>
        /// Tên loại cây trồng. Bắt buộc.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Mô tả loại cây trồng (tùy chọn).
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Trạng thái hoạt động.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
