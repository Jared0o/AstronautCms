using AstronautCms.Modules.Users.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AstronautCms.Modules.Users.Infrastructure.DbContext;

public class UserDbContext : IdentityDbContext<User, Role, Guid>
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.HasDefaultSchema("Users");
    }
}