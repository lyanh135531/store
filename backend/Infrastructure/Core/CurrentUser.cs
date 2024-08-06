using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Core;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public string Id => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    public string UserName => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
    public string Email => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
}