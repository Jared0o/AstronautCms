using AstronautCms.Shared.Abstract.Result;
using Mediator;

namespace AstronautCms.Modules.Users.Core.Commands;

public record CreateUserCommand(string Email, string Password, string PasswordConfirm) : IRequest<Result>;
