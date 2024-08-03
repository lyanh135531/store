using Domain.Core;
using Microsoft.AspNetCore.Identity;

namespace Domain.Ums.Entities;

public class User : IdentityUser<Guid>, IEntity<Guid>
{
    public override string UserName { get; set; }
    public override string Email { get; set; }
    public string FullName { get; set; }
    public Gender Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public bool Status { get; set; } = true;
    public DateTime CreatedAt { get; set; }

    public List<UserRole> UserRoles { get; set; } = new();
}