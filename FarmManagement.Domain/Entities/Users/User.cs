using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Users;

/// <summary>
/// Đại diện cho người dùng trong hệ thống.
/// </summary>
public class User : AuditableEntity
{
    /// <summary>
    /// Mã định danh của người dùng.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Tên đăng nhập của người dùng.
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Mật khẩu đã được mã hóa của người dùng.
    /// </summary>
    public string PasswordHash { get; set; } = null!;

    /// <summary>
    /// Họ và tên đầy đủ của người dùng.
    /// </summary>
    public string FullName { get; set; } = null!;

    /// <summary>
    /// Số điện thoại của người dùng (nếu có).
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Địa chỉ email của người dùng (nếu có).
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Trạng thái hoạt động của người dùng.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Ngày tạo tài khoản.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Ngày cập nhật tài khoản gần nhất (nếu có).
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Danh sách vai trò của người dùng.
    /// </summary>
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}