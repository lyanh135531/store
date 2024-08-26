using System.Security.Claims;
using Api.DTOs;
using Application.Ums.DTOs;
using AutoMapper;
using Domain.Ums.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Identity;

[ApiController]
[Route("api/admin/identity")]
public class IdentityController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (!ModelState.IsValid) return Unauthorized();

        var user = await userManager.FindByNameAsync(model.UserName);
        if (user == null || !await userManager.CheckPasswordAsync(user, model.Password) ||
            !await userManager.IsInRoleAsync(user, Role.Admin))
        {
            return Unauthorized();
        }

        var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe,
            lockoutOnFailure: false);
        if (!result.Succeeded) return Unauthorized();

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, model.UserName)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

        var userProfile = mapper.Map<UserProfileDto>(user);
        return Ok(userProfile);
    }

    [HttpGet("check-login")]
    public async Task<IActionResult> CheckLogin()
    {
        if (User.Identity is not { IsAuthenticated: true }) return Unauthorized();
        var userName = User.Identity.Name;

        if (string.IsNullOrEmpty(userName)) return Unauthorized();

        var user = await userManager.FindByNameAsync(userName);
        var userProfile = mapper.Map<UserProfileDto>(user);

        return Ok(new
        {
            Success = true,
            Result = userProfile
        });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Ok();
    }
}