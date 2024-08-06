namespace Application.Ums.DTOs;

public class UserProfileDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}