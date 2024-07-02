using Application.Core.Services;
using Application.Ums.DTOs;
using AutoMapper;
using Domain.Core;
using Domain.Ums.Entities;
using Domain.Ums.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Application.Ums.Services;

public class UserService : AppServiceBase<User, Guid, UserListDto, UserDetailDto, UserCreateDto, UserUpdateDto>,
    IUserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private const string PasswordAdmin = "Admin@123";

    public UserService(IUserRepository userRepository, IMapper mapper, UserManager<User> userManager) : base(
        userRepository, mapper)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<UserDetailDto> RegisterAdmin()
    {
        var userAdmin = new User
        {
            UserName = "admin",
            Email = "admin@gmail.com",
            FullName = "Admin",
            UserRoles = new List<UserRole>
            {
                new() { Role = new Role { Name = "Admin System", Code = Role.SystemAdminRole, Type = RoleType.Admin } }
            }
        };
        await _userManager.CreateAsync(userAdmin, PasswordAdmin);

        return _mapper.Map<User, UserDetailDto>(userAdmin);
    }
}