using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Application.DTOs.Locations
{
    /// <summary>
    /// DTO đại diện cho trạng thái của Location.
    /// </summary>
    public class LocationStatusDto
    {
        /// <summary>
        /// ID của trạng thái Location.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Mã trạng thái Location.
        /// </summary>
        [Required(ErrorMessage = "Code không được để trống")]
        [StringLength(50)]
        public string Code { get; set; } = null!;

        /// <summary>
        /// Tên trạng thái Location.
        /// </summary>
        [Required(ErrorMessage = "Name không được để trống")]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Ngày tạo trạng thái Location.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Ngày cập nhật trạng thái Location gần nhất.
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
