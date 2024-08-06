using Application.Ums.DTOs;
using AutoMapper;
using Domain.Ums.Entities;

namespace Application.Ums.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserListDto>();
        CreateMap<User, UserDetailDto>();
        CreateMap<User, UserProfileDto>();

        CreateMap<UserUpdateDto, User>();
        CreateMap<UserCreateDto, User>();
    }
}