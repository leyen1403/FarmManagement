namespace FarmManagement.Application.DTOs.Locations
{
    /// <summary>
    /// DTO đại diện cho thông tin Location.
    /// </summary>
    public class LocationDto
    {
        /// <summary>
        /// ID của Location.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tên của Location.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// ID loại Location.
        /// </summary>
        public int LocationTypeId { get; set; }

        /// <summary>
        /// Tên loại Location (dùng để hiển thị).
        /// </summary>
        public string? LocationTypeName { get; set; }

        /// <summary>
        /// ID trạng thái Location.
        /// </summary>
        public int LocationStatusId { get; set; }

        /// <summary>
        /// Tên trạng thái Location (dùng để hiển thị).
        /// </summary>
        public string? LocationStatusName { get; set; }

        /// <summary>
        /// Địa chỉ Location.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Mô tả Location.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Diện tích Location.
        /// </summary>
        public decimal? Area { get; set; }

        /// <summary>
        /// Sức chứa Location.
        /// </summary>
        public decimal? Capacity { get; set; }

        /// <summary>
        /// Đơn vị của sức chứa.
        /// </summary>
        public string? CapacityUnit { get; set; }

        /// <summary>
        /// Ngày bắt đầu sử dụng Location.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Ngày kết thúc sử dụng Location.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// ID của Location cha (nếu có).
        /// </summary>
        public int? ParentLocationId { get; set; }

        /// <summary>
        /// Ghi chú bổ sung về Location.
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Ngày tạo Location.
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Ngày cập nhật Location gần nhất.
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
    }
}
