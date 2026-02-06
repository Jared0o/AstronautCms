using AstronautCms.Modules.Users.Core.Dtos;
using AstronautCms.Modules.Users.Core.Models;
using AstronautCms.Shared.Abstract.Result;

namespace AstronautCms.Modules.Users.Core.Repositories;

public interface IUserRepository
{
    Task<Result> CreateUserAsync(User user, string password, CancellationToken ct = default);
    Task<Result<User>> CheckUserAndPasswordAsync(string email, string password, CancellationToken ct = default);
}