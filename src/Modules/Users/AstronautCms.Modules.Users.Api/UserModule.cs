using AstronautCms.Modules.Users.Core;
using AstronautCms.Modules.Users.Infrastructure;
using AstronautCms.Shared.Abstract.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AstronautCms.Modules.Users.Api;

public class UserModule : IModule
{
    public string Name { get; } = "Users";
    public string Path { get; } = "users";
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddUsersCore();
        services.AddUserInfrastructure(configuration);
    }

    public void Use(WebApplication app)
    {
        app.MapGroup($"/{Path}").WithTags(Name).MapGet("/", () => "Users Module is running.");
    }
}