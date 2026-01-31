using AstronautCms.Modules.Users.Core.Dtos;
using AstronautCms.Modules.Users.Core.Models;
using AstronautCms.Modules.Users.Core.Repositories;
using AstronautCms.Modules.Users.Core.Services;
using AstronautCms.Shared.Abstract.Result;
using AstronautCms.Shared.Abstract.Result.CustomErrors;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace AstronautCms.Modules.Users.Core.Commands.LogInUser;

public sealed class LogInUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<LogInUserDto> _validator;
    private readonly ITokenGenerator _tokenGenerator;

    public LogInUserUseCase(IUserRepository userRepository, IValidator<LogInUserDto> validator, ITokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _validator = validator;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<Result<TokenDto>> Execute(LogInUserDto request, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(request, ct);

        if (!validationResult.IsValid)
        {
            var validationError = validationResult.ToValidationError();
            return Result<TokenDto>.Failure(validationError);
        }

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Result<TokenDto>.Failure(new NotFoundError("User not found."));
        }
        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordValid)
        {
            return Result<TokenDto>.Failure(new UnauthorizedError("Invalid password."));
        }

        var token = await _tokenGenerator.GenerateTokenAsync(user);
        
        return Result<TokenDto>.Success(new TokenDto(token));

    } 
}