namespace FarmManagement.Domain.Entities.Users;

/// <summary>
/// Đại diện cho vai trò của người dùng trong hệ thống.
/// </summary>
public class Role
{
    /// <summary>
    /// Mã định danh của vai trò.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Mã vai trò.
    /// </summary>
    public string RoleCode { get; set; } = null!;

    /// <summary>
    /// Tên vai trò.
    /// </summary>
    public string RoleName { get; set; } = null!;

    /// <summary>
    /// Danh sách người dùng có vai trò này.
    /// </summary>
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}