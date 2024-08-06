using Api.DTOs;
using Application.Ums.DTOs;
using AutoMapper;
using Domain.Ums.Entities;
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
        if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
        {
            return Unauthorized();
        }

        var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe,
            lockoutOnFailure: false);
        if (!result.Succeeded) return Unauthorized();

        var userProfile = mapper.Map<UserProfileDto>(user);
        return Ok(userProfile);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Ok();
    }
}