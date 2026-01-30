using FluentValidation;

namespace AstronautCms.Modules.Users.Core.Commands.LogInUser;

public class LogInUserDtoValidator : AbstractValidator<LogInUserDto>
{
    public LogInUserDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Please provide a valid email address.");
    }
}