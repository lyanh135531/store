using Application.Core.Services;
using Application.Ums.DTOs;

namespace Application.Ums.Services;

public interface IUserService : IAppServiceBase<Guid, UserListDto, UserDetailDto, UserCreateDto, UserUpdateDto>
{
    Task<UserDetailDto> RegisterAdmin();
}