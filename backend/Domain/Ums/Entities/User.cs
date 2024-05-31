using Domain.Core;
using Microsoft.AspNetCore.Identity;

namespace Domain.Ums.Entities;

public class User : IdentityUser<Guid>
{
    public string FullName { get; set; }
    public Gender Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}