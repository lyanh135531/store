using Application.Core.DTOs;
using Application.Core.Services;
using Application.Ums.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Core;
using Domain.Ums.Entities;
using Domain.Ums.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Ums.Services;

public class UserService(
    IRepository<User, Guid> repository,
    IDistributedCache distributedCache,
    IMapper mapper,
    UserManager<User> userManager,
    RoleManager<Role> roleManager,
    IRoleRepository roleRepository)
    : AppServiceBase<User, Guid, UserListDto, UserDetailDto, UserCreateDto, UserUpdateDto>(repository, distributedCache,
        mapper), IUserService
{
    private readonly IMapper _mapper = mapper;
    private const string PasswordAdmin = "Admin@123";
    private const string EmailAdmin = "admin@gmail.com";

    public async Task<UserDetailDto> RegisterAdmin()
    {
        var userAdminExist = await userManager.FindByEmailAsync(EmailAdmin);
        if (userAdminExist != null) throw new Exception("User admin is exist");

        var roleAdminExist = await roleManager.FindByNameAsync(Role.Admin);
        if (roleAdminExist != null) throw new Exception("Role admin is exist");

        var roleAdmin = new Role
        {
            Name = Role.Admin,
            Code = Role.SystemAdminRoleCode,
            Type = RoleType.Admin,
        };

        await roleManager.CreateAsync(roleAdmin);

        var userAdmin = new User
        {
            UserName = "admin",
            Email = "admin@gmail.com",
            FullName = "Admin",
        };
        await userManager.CreateAsync(userAdmin, PasswordAdmin);
        await userManager.AddToRoleAsync(userAdmin, Role.Admin);
        await Repository.UpdateAsync(userAdmin, true);

        return _mapper.Map<User, UserDetailDto>(userAdmin);
    }

    public override async Task<UserDetailDto> CreateAsync(UserCreateDto userCreateDto)
    {
        var user = _mapper.Map<UserCreateDto, User>(userCreateDto);
        await userManager.CreateAsync(user, userCreateDto.Password);
        await Repository.UpdateAsync(user, true);

        return _mapper.Map<User, UserDetailDto>(user);
    }

    public override async Task<PaginatedList<UserListDto>> GetListAsync(PaginatedListQuery query,
        CancellationToken cancellationToken = default)
    {
        var roleQueryable = await roleRepository.GetQueryableAsync();
        var userAdminId = await roleQueryable
            .Where(x => x.Code == Role.SystemAdminRoleCode)
            .SelectMany(x => x.UserRoles)
            .Select(x => x.UserId)
            .ToListAsync(cancellationToken);

        var queryable = await Repository.GetQueryableAsync();
        queryable = queryable
            .Where(x => !userAdminId.Contains(x.Id));

        var total = await queryable.CountAsync(cancellationToken);

        var result = await queryable
            .ProjectTo<UserListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new PaginatedList<UserListDto>(result, total, query.Offset, query.Limit);
    }
}