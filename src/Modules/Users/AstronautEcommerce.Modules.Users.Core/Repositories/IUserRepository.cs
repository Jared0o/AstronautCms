using AstronautEcommerce.Modules.Users.Core.Models;
using AstronautEcommerce.Shared.Abstract.Result;

namespace AstronautEcommerce.Modules.Users.Core.Repositories;

public interface IUserRepository
{
    Task<Result> CreateUserAsync(User user, string password, CancellationToken ct = default);
    Task<Result<User>> CheckUserAndPasswordAsync(string email, string password, CancellationToken ct = default);
}