using AstronautEcommerce.Shared.Abstract.Result;

namespace AstronautEcommerce.Modules.Users.Core.Errors;

public record InvalidCredentialsError : Error
{
    public InvalidCredentialsError() : base("Invalid email or password.")
    {
    }
}