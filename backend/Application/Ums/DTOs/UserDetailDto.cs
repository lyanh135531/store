using Domain.Core;

namespace Application.Ums.DTOs;

public class UserDetailDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string FullName { get; set; }
    public string Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }
}