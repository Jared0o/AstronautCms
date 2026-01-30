using AstronautCms.Modules.Users.Core.Dtos;
using AstronautCms.Modules.Users.Core.Models;
using AstronautCms.Shared.Abstract.Result;
using AstronautCms.Shared.Abstract.Result.CustomErrors;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace AstronautCms.Modules.Users.Core.Commands.LogInUser;

public sealed class LogInUserUseCase
{
    private readonly UserManager<User> _userManager;
    private readonly IValidator<LogInUserDto> _validator;

    public LogInUserUseCase(UserManager<User> userManager, IValidator<LogInUserDto> validator)
    {
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<Result<TokenDto>> Execute(LogInUserDto request, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(request, ct);

        if (!validationResult.IsValid)
        {
            var validationError = validationResult.ToValidationError();
            return Result<TokenDto>.Failure(validationError);
        }
        
        
        
    } 
}