namespace AstronautCms.Shared.Abstract.Result.CustomErrors;

public record UnauthorizedError : Error
{
    public UnauthorizedError(string message) : base(message)
    {
    }
}