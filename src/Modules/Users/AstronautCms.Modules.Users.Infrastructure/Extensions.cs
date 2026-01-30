using AstronautCms.Modules.Users.Core.Models;
using AstronautCms.Modules.Users.Core.Repositories;
using AstronautCms.Modules.Users.Infrastructure.DbContext;
using AstronautCms.Modules.Users.Infrastructure.Repositories;
using AstronautCms.Shared.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AstronautCms.Modules.Users.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddUserInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        
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