using AstronautCms.Modules.Users.Core.Models;
using AstronautCms.Modules.Users.Core.Repositories;
using AstronautCms.Shared.Abstract.Result;
using AstronautCms.Shared.Abstract.Result.CustomErrors;
using FluentValidation;
using Mediator;

namespace AstronautCms.Modules.Users.Core.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
{
    private readonly IValidator<CreateUserCommand> _validator;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IValidator<CreateUserCommand> validator, IUserRepository userRepository)
    {
        _validator = validator;
        _userRepository = userRepository;
    }
    
    public async ValueTask<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var validationError = validationResult.ToValidationError();
            return Result.Failure(validationError);
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            UserName = request.Email
        };
        
        return await _userRepository.CreateUserAsync(user, request.Password);
    }
}