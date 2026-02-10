using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AstronautEcommerce.Shared.Abstract.Modules;

public interface IModule
{
    string Name { get; }
    string RoutePrefix { get; }
    bool Enabled { get; }

    void RegisterServices(IServiceCollection services, IConfiguration configuration);
    void RegisterEndpoints(RouteGroupBuilder routeBuilder);
    Task RegisterMiddleware(IApplicationBuilder app);
}