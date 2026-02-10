using System.Reflection;
using AstronautEcommerce.Modules.Users.Core.Commands.CreateUser;
using AstronautEcommerce.Modules.Users.Core.Commands.LogInUser;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AstronautEcommerce.Modules.Users.Core;

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