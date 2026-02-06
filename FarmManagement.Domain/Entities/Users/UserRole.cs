using FarmManagement.Domain.Common;

namespace FarmManagement.Domain.Entities.Users;

/// <summary>
/// Liên kết giữa người dùng và vai trò của họ.
/// </summary>
public class UserRole : AuditableEntity
{
    /// <summary>
    /// Mã định danh của liên kết.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã định danh của người dùng.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Mã định danh của vai trò.
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    /// Người dùng liên kết.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Vai trò liên kết.
    /// </summary>
    public Role Role { get; set; } = null!;
}