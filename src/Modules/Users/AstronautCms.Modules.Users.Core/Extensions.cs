using System.Reflection;
using AstronautCms.Modules.Users.Core.Commands.CreateUser;
using AstronautCms.Modules.Users.Core.Commands.LogInUser;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AstronautCms.Modules.Users.Core;

public static class Extensions
{
    public static IServiceCollection AddUsersCore(this IServiceCollection services)
    {
        services.AddScoped<CreateUserUseCase>();
        services.AddScoped<LogInUserUseCase>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}