using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AstronautCms.Shared.Infrastructure.Database;

public static class Extensions
{
    public static IServiceCollection AddPostgres<T>(this IServiceCollection services, DatabaseOptions databaseOptions) where T : DbContext
    {
        services.AddDbContext<T>(opt =>
        {
            opt.UseNpgsql(databaseOptions.ConnectionString);
        });
        
        return services;
    }
}