namespace Application.Ums.DTOs;

public class UserProfileDto
{
    public Guid Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public string? FullName { get; set; }
}