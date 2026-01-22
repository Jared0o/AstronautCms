using FluentValidation;

namespace AstronautCms.Modules.Users.Core.Commands;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email jest wymagany.")
            .EmailAddress().WithMessage("Podaj poprawny adres email.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Hasło jest wymagane.")
            .MinimumLength(6).WithMessage("Hasło musi mieć co najmniej 6 znaków.")
            .Matches("[A-Z]").WithMessage("Hasło musi zawierać co najmniej jedną wielką literę.")
            .Matches("[a-z]").WithMessage("Hasło musi zawierać co najmniej jedną małą literę.")
            .Matches("[0-9]").WithMessage("Hasło musi zawierać co najmniej jedną cyfrę.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Hasło musi zawierać co najmniej jeden znak specjalny.");

        RuleFor(x => x.PasswordConfirm)
            .NotEmpty().WithMessage("Potwierdzenie hasła jest wymagane.")
            .Equal(x => x.Password).WithMessage("Hasła muszą być takie same.");
    }
}