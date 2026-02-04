using AstronautCms.Modules.Users.Core.Dtos;
using AstronautCms.Modules.Users.Core.Models;
using AstronautCms.Shared.Abstract.Result;

namespace AstronautCms.Modules.Users.Core.Repositories;

public interface IUserRepository
{
    Task<Result> CreateUserAsync(User user, string password);
    Task<Result<User>> LogInUserAsync(string email, string password);
    Task<Result<bool>> UserExistsAsync(string email);
}