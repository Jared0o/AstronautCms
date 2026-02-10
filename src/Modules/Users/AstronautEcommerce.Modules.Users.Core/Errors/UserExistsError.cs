
using AstronautEcommerce.Shared.Abstract.Result;

namespace AstronautEcommerce.Modules.Users.Core.Errors;

public sealed record UserExistsError : Error
{
    public UserExistsError() : base("User with the given email already exists.")
    {
    }
}