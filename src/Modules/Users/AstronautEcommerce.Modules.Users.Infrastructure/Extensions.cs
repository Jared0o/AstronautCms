using AstronautEcommerce.Modules.Users.Core.Models;
using AstronautEcommerce.Modules.Users.Core.Repositories;
using AstronautEcommerce.Modules.Users.Core.Services;
using AstronautCms.Shared.Infrastructure.Database;
using AstronautEcommerce.Modules.Users.Infrastructure.DbContext;
using AstronautEcommerce.Modules.Users.Infrastructure.Repositories;
using AstronautEcommerce.Modules.Users.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AstronautEcommerce.Modules.Users.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddUserInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        
        services.AddPostgres<UserDbContext>(new DatabaseOptions()
        {
            ConnectionString = configuration.GetConnectionString("UsersContext") ?? throw new InvalidOperationException("Connection string 'UsersContext' not found."),
        });

        services.AddIdentityCore<User>(x =>
            {
                x.Password.RequireDigit = true;
                x.Password.RequireLowercase = true;
                x.Password.RequireUppercase = true;
                x.Password.RequireNonAlphanumeric = true;
                x.Password.RequiredLength = 8;
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<UserDbContext>()
            .AddSignInManager<SignInManager<User>>()
            .AddRoleManager<RoleManager<Role>>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}