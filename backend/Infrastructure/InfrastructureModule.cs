using Domain.Business.Repositories;
using Domain.Ums.Repositories;
using Infrastructure.Core;
using Infrastructure.Repositories.Business;
using Infrastructure.Repositories.Ums;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureModule
{
    public static void AddInfrastructure(this IServiceCollection service)
    {
        service.AddScoped<ICurrentUser, CurrentUser>();

        #region Ums

        service.AddTransient<IUserRepository, UserRepository>();
        service.AddTransient<IRoleRepository, RoleRepository>();

        #endregion

        #region Business

        service.AddTransient<IProductRepository, ProductRepository>();
        service.AddTransient<IOrderRepository, OrderRepository>();
        service.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
        service.AddTransient<ICategoryRepository, CategoryRepository>();

        #endregion
    }
}