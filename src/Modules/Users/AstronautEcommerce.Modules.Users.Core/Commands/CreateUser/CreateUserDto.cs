namespace AstronautEcommerce.Modules.Users.Core.Commands.CreateUser;

public record CreateUserDto(string Email, string Password, string PasswordConfirm);
