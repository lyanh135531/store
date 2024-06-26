using Microsoft.AspNetCore.Identity;

namespace Domain.Ums.Entities;

public class RoleClaim : IdentityRoleClaim<Guid>
{
    public Role Role { get; set; }
}