using AstronautCms.Modules.Users.Core.Models;
using AstronautCms.Modules.Users.Core.Repositories;
using AstronautCms.Shared.Abstract.Result;
using AstronautCms.Shared.Abstract.Result.CustomErrors;
using FluentValidation;

namespace AstronautCms.Modules.Users.Core.Commands.CreateUser;

public class CreateUserUseCase
{
    private readonly IValidator<CreateUserDto> _validator;
    private readonly IUserRepository _userRepository;

    public CreateUserUseCase(IValidator<CreateUserDto> validator, IUserRepository userRepository)
    {
        _validator = validator;
        _userRepository = userRepository;
    }
    
    public async Task<Result> Execute(CreateUserDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
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
        
        return await _userRepository.CreateUserAsync(user, request.Password);
    }
}