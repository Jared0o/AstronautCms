using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AstronautCms.Shared.Abstract.Modules;

public interface IModule
{
    string Name { get; }
    string Path { get; }

    void Register(IServiceCollection services, IConfiguration configuration);
    void Use(WebApplication app);
}