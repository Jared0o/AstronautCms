using AstronautCms.Shared.Abstract.Result;

namespace AstronautCms.Modules.Users.Core.Errors;

public record InvalidCredentialsError : Error
{
    public InvalidCredentialsError() : base("Invalid email or password.")
    {
    }
}