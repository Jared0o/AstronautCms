using Microsoft.AspNetCore.Identity;

namespace AstronautEcommerce.Modules.Users.Core.Models;

public class User : IdentityUser<Guid>
{
    public override required string Email { get; set; }
}