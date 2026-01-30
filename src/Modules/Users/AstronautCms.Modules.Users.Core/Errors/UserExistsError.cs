using AstronautCms.Shared.Abstract.Result;

namespace AstronautCms.Modules.Users.Core.Errors;

public sealed record UserExistsError : Error
{
    public UserExistsError() : base("User with the given email already exists.")
    {
    }
}