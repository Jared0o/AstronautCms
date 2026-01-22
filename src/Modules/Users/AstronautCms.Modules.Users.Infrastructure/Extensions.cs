using AstronautCms.Modules.Users.Infrastructure.DbContext;
using AstronautCms.Shared.Infrastructure.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AstronautCms.Modules.Users.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddUserInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres<UserDbContext>(new DatabaseOptions()
        {
            ConnectionString = configuration.GetConnectionString("UsersContext") ?? throw new InvalidOperationException("Connection string 'UsersContext' not found."),
        });
        
        return services;
    }
}