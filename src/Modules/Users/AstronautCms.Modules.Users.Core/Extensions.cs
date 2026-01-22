using Microsoft.Extensions.DependencyInjection;

namespace AstronautCms.Modules.Users.Core;

public static class Extensions
{
    public static IServiceCollection AddUsersCore(this IServiceCollection services)
    {
        services.AddMediator();

        return services;
    }
}