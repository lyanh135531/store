using Api.Core;
using Application.Core.DTOs;
using Application.Ums.DTOs;
using Application.Ums.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Ums;

[ApiController]
[Route("/api/user")]
public class UserController : ApiControllerBase<Guid, UserListDto, UserDetailDto, UserCreateDto, UserUpdateDto>
{
    private readonly IUserService _userService;

    public UserController(IUserService userService) : base(userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ApiResponse<UserDetailDto>> RegisterAdmin()
    {
        var result = await _userService.RegisterAdmin();

        return ApiResponse<UserDetailDto>.Ok(result);
    }
}