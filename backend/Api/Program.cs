using System.Reflection;
using Api.Middlewares;
using Application;
using Application.Configs;
using Domain.Ums.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Common;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddApplication();
services.AddInfrastructure();
services.AddControllers();

services.AddHttpContextAccessor();
services.AddSingleton<DatabaseUpdater>();
services.Configure<FileConfig>(configuration.GetSection("FileConfig"));
builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = configuration["RedisCacheUrl"]; });

#region Serilog

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Host.UseSerilog();

#endregion

#region Validator

services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();
services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

#endregion

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

#region Identity

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

#endregion

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

#region SwaggerGen

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Store", Version = "v1" });
    c.CustomSchemaIds(i => i.FullName);
});

#endregion

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

#region DbUp

using (var scope = app.Services.CreateScope())
{
    try
    {
        var databaseUpdater = scope.ServiceProvider.GetRequiredService<DatabaseUpdater>();
        databaseUpdater.Upgrade();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
    }
}

#endregion

#region StaticFiles

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "UploadFiles")),
});

#endregion

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();