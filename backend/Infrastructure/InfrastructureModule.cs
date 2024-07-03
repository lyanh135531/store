using Domain.Ums.Repositories;
using Infrastructure.Repositories.Ums;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureModule
{
    public static void AddInfrastructure(this IServiceCollection service)
    {
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IRoleRepository, RoleRepository>();
    }
}