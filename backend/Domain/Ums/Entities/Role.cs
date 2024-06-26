using Domain.Core;
using Microsoft.AspNetCore.Identity;

namespace Domain.Ums.Entities;

public class Role : IdentityRole<Guid>
{
    public string Code { get; set; }
    public RoleType Type { get; set; }

    public List<UserRole> UserRoles { get; set; } = new();

    public List<RoleClaim> RoleClaims { get; set; } = new();
}