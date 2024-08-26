using Domain.Core;
using Microsoft.AspNetCore.Identity;

namespace Domain.Ums.Entities;

public class Role : IdentityRole<Guid>, IEntity<Guid>
{
    /// <summary>
    /// Role Code
    /// </summary>
    public const string SystemAdminRoleCode = "SYSTEM_ADMIN_ROLE";

    /// <summary>
    /// Role Name
    /// </summary>
    public const string Admin = "Admin";

    public required string Code { get; set; }
    public RoleType Type { get; set; }

    public List<UserRole> UserRoles { get; set; } = new();

    public List<RoleClaim> RoleClaims { get; set; } = new();
}