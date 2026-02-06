using AstronautCms.Modules.Users.Core.Models;
using AstronautCms.Modules.Users.Core.Services;

namespace AstronautCms.Modules.Users.Infrastructure.Services;

public class TokenGenerator : ITokenGenerator
{
    public Task<string> GenerateTokenAsync(User user, CancellationToken ct = default)
    {
        
    }
}