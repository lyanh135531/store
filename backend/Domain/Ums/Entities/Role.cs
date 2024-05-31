using Domain.Core;

namespace Domain.Ums.Entities;

public class Role : Entity<Guid>
{
    public string Code { get; set; }
    public RoleType Type { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}