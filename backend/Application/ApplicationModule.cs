using Application.Ums.Mapper;
using Application.Ums.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationModule
{
    public static void AddApplication(this IServiceCollection service)
    {
        var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile<UserMapper>(); });
        var mapper = mapperConfig.CreateMapper();
        service.AddSingleton(mapper);

        service.AddScoped<IUserService, UserService>();
    }
}