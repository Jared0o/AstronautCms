using AstronautCms.Modules.Users.Core.Models;

namespace AstronautCms.Modules.Users.Core.Services;

public interface ITokenGenerator
{
    public Task<string> GenerateTokenAsync(User user, CancellationToken ct = default);
}