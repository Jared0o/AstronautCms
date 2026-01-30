using Microsoft.AspNetCore.Identity;

namespace AstronautCms.Modules.Users.Core.Models;

public class Role : IdentityRole<Guid>
{
    
}

public enum RoleNames
{
    Admin,
    Moderator,
    User
}