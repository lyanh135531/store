using Api.Core;
using Application.Core.DTOs;
using Application.Ums.DTOs;
using Application.Ums.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Ums;

[ApiController]
[Route("/api/admin/user")]
[Authorize]
public class UserController(IUserService userService)
    : ApiControllerBase<Guid, UserListDto, UserDetailDto, UserCreateDto, UserUpdateDto>(userService)
{
    [HttpPost("register")]
    public async Task<ApiResponse<UserDetailDto>> RegisterAdmin()
    {
        var result = await userService.RegisterAdmin();

        return ApiResponse<UserDetailDto>.Ok(result);
    }
}