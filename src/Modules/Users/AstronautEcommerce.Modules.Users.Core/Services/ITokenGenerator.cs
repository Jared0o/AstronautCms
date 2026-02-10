using AstronautEcommerce.Modules.Users.Core.Models;

namespace AstronautEcommerce.Modules.Users.Core.Services;

public interface ITokenGenerator
{
    public Task<string> GenerateTokenAsync(User user, CancellationToken ct = default);
}