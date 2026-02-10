namespace AstronautEcommerce.Shared.Abstract.Result.CustomErrors;

public record NotFoundError : Error
{
    public NotFoundError(string message) : base(message)
    {
    }
}