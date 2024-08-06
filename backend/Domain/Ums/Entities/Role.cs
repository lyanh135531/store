using Domain.Core;
using Microsoft.AspNetCore.Identity;

namespace Domain.Ums.Entities;

public class Role : IdentityRole<Guid>, IEntity<Guid>
{
    public const string SystemAdminRoleCode = "SYSTEM_ADMIN_ROLE";
    public const string Admin = "Admin";
    public string Code { get; set; }
    public RoleType Type { get; set; }

    public List<UserRole> UserRoles { get; set; } = new();

    public List<RoleClaim> RoleClaims { get; set; } = new();
}