using Application.Business.Services.Products;
using Application.Ums.Mapper;
using Application.Ums.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationModule
{
    public static void AddApplication(this IServiceCollection service)
    {
        #region Mapper

        var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile<UserMapper>(); });
        var mapper = mapperConfig.CreateMapper();
        service.AddSingleton(mapper);

        #endregion

        #region Ums

        service.AddTransient<IUserService, UserService>();

        #endregion

        #region Business

        service.AddTransient<IProductService, ProductService>();

        #endregion
    }
}