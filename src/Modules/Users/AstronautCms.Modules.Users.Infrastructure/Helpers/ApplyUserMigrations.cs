using AstronautCms.Modules.Users.Core.Models;
using AstronautCms.Modules.Users.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AstronautCms.Modules.Users.Infrastructure.Helpers;

public static class ApplyUserMigrations
{
    public static async Task ApplyMigrations(IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        await dbContext.Database.MigrateAsync();
        
        await SeedRoles(scope.ServiceProvider);
    }
    
    private static async Task SeedRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
        var roleNames = Enum.GetNames<RoleNames>();
        
        foreach (var roleName in roleNames)
        {
            if (await roleManager.RoleExistsAsync(roleName)) continue;
            var role = new Role { Name = roleName };
            await roleManager.CreateAsync(role);
        }
    }
}