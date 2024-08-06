namespace Infrastructure.Core;

public interface ICurrentUser
{
    public string Id { get; }
    public string UserName { get; }
    public string Email { get; }
}