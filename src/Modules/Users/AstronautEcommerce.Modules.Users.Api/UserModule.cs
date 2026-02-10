using AstronautEcommerce.Modules.Users.Api.Routes;
using AstronautEcommerce.Modules.Users.Core;
using AstronautEcommerce.Modules.Users.Infrastructure;
using AstronautEcommerce.Modules.Users.Infrastructure.Helpers;
using AstronautEcommerce.Shared.Abstract.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AstronautEcommerce.Modules.Users.Api;

public class UserModule : IModule
{
    public string Name => "Users";
    public string RoutePrefix => "users";
    public bool Enabled => true;

    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        => services.AddUsersCore()
            .AddUserInfrastructure(configuration);

    public void RegisterEndpoints(RouteGroupBuilder routeBuilder)
        => routeBuilder.MapUserEndpoints();

    public async Task RegisterMiddleware(IApplicationBuilder app)
    {
        await using var scope = app.ApplicationServices.CreateAsyncScope();
        var serviceProvider = scope.ServiceProvider;
        await ApplyUserMigrations.ApplyMigrations(serviceProvider);
    }
}