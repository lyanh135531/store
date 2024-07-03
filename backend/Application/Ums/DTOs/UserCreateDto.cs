using Domain.Core;

namespace Application.Ums.DTOs;

public class UserCreateDto
{
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
}