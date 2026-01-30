using AstronautCms.Modules.Users.Api.Routes;
using AstronautCms.Modules.Users.Core;
using AstronautCms.Modules.Users.Infrastructure;
using AstronautCms.Shared.Abstract.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AstronautCms.Modules.Users.Api;

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

    public Task RegisterMiddleware(IApplicationBuilder app)
    {
        // No middlewares to register for this module
        return Task.CompletedTask;
    }
}