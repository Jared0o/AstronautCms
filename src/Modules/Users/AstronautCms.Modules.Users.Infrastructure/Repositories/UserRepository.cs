using AstronautCms.Modules.Users.Core.Errors;
using AstronautCms.Modules.Users.Core.Models;
using AstronautCms.Modules.Users.Core.Repositories;
using AstronautCms.Shared.Abstract.Result;
using AstronautCms.Shared.Abstract.Result.CustomErrors;
using Microsoft.AspNetCore.Identity;

namespace AstronautCms.Modules.Users.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<Result> CreateUserAsync(User user, string password, CancellationToken ct = default)
    {
         var userExists = await _userManager.FindByEmailAsync(user.Email);
         
         if (userExists != null)
         {
             return Result.Failure(new UserExistsError());
         }
         
         var result = await _userManager.CreateAsync(user, password);
         
         if (!result.Succeeded)
         {
             return Result.Failure(new BaseError("Failed to create user."));
         }
         
         await _userManager.AddToRoleAsync(user, nameof(RoleNames.User));
         
            return Result.Success();
    }

    public async Task<Result<User>> CheckUserAndPasswordAsync(string email, string password, CancellationToken ct = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user == null)
        {
            return Result<User>.Failure(new NotFoundError("User not found."));
        }
        
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        
        if (!isPasswordValid)
        {
            return Result<User>.Failure(new InvalidCredentialsError());
        }
        
        return Result<User>.Success(user);
    }
}