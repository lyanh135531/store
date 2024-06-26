using Domain.Core;
using Microsoft.AspNetCore.Identity;

namespace Domain.Ums.Entities;

public class User : IdentityUser<Guid>, IEntity<Guid>
{
    public string FullName { get; set; }
    public Gender Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<UserRole> UserRoles { get; set; } = new();
}