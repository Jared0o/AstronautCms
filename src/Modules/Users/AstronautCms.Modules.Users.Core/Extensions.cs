using System.Reflection;
using AstronautCms.Modules.Users.Core.Commands.CreateUser;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AstronautCms.Modules.Users.Core;

public static class Extensions
{
    public static IServiceCollection AddUsersCore(this IServiceCollection services)
    {
        services.AddScoped<CreateUserUseCase>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}