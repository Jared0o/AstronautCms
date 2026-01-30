using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace AstronautCms.Modules.Users.Api.Routes;

public static class UserEndpoints
{
    internal static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", () => "User Module is working!");
        return group;
    }
}