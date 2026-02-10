using AstronautEcommerce.Modules.Users.Core.Models;
using AstronautEcommerce.Modules.Users.Core.Repositories;
using AstronautEcommerce.Shared.Abstract.Result;
using AstronautEcommerce.Shared.Abstract.Result.CustomErrors;
using FluentValidation;

namespace AstronautEcommerce.Modules.Users.Core.Commands.CreateUser;

public class CreateUserUseCase
{
    private readonly IValidator<CreateUserDto> _validator;
    private readonly IUserRepository _userRepository;

    public CreateUserUseCase(IValidator<CreateUserDto> validator, IUserRepository userRepository)
    {
        _validator = validator;
        _userRepository = userRepository;
    }
    
    public async Task<Result> Execute(CreateUserDto request, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            var validationError = validationResult.ToValidationError();
            return Result.Failure(validationError);
        }

        var user = new User
        {
            Id = Guid.CreateVersion7(),
            Email = request.Email,
            UserName = request.Email
        };
        
        return await _userRepository.CreateUserAsync(user, request.Password, ct);
    }
}