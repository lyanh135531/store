using System.Reflection;
using Application;
using Domain.Ums.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Common;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddApplication();
services.AddInfrastructure();
services.AddControllers();

#region Validator

services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();
services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

#endregion

services.AddHttpContextAccessor();

#region DbContext

services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("Default"));
});
services.AddDbContext<IdentityContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("Default"));
});

#endregion

services.AddIdentity<User, Role>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 6;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

#region Cookie

services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "STORE";
    options.LoginPath = "/api/identity/login";
    options.LogoutPath = "/api/identity/logout";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

#endregion

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Store", Version = "v1" });
    c.CustomSchemaIds(i => i.FullName);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store Api");
        c.RoutePrefix = string.Empty;
    });
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var seedFolderPath = Path.Combine(app.Environment.ContentRootPath, "../Migrator/Script");
    SeedData.Seed(context, seedFolderPath);
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();