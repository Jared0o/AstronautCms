using AstronautEcommerce.Modules.Users.Core.Dtos;
using AstronautEcommerce.Modules.Users.Core.Repositories;
using AstronautEcommerce.Modules.Users.Core.Services;
using AstronautEcommerce.Shared.Abstract.Result;
using AstronautEcommerce.Shared.Abstract.Result.CustomErrors;
using FluentValidation;

namespace AstronautEcommerce.Modules.Users.Core.Commands.LogInUser;

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
            return Result<TokenDto>.Failure(validationResult.ToValidationError());

        var userResult = await _userRepository.CheckUserAndPasswordAsync(request.Email, request.Password, ct);

        if (userResult is not { IsSuccess: true, Value: { } user })
            return Result<TokenDto>.Failure(userResult.Error ?? new BaseError("Something went wrong during login."));

        var token = await _tokenGenerator.GenerateTokenAsync(user, ct);

        return Result<TokenDto>.Success(new TokenDto(token));
    }

}