using System.Reflection;
using Application.Business.Services.Categories;
using Application.Business.Services.Products;
using Application.Core.Services;
using Application.Ums.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationModule
{
    public static void AddApplication(this IServiceCollection service)
    {
        service.AddAutoMapper(Assembly.GetExecutingAssembly());

        #region Ums

        service.AddTransient<IUserService, UserService>();

        #endregion

        #region Business

        service.AddTransient<IProductService, ProductService>();
        service.AddTransient<ICategoryService, CategoryService>();

        #endregion
    }
}