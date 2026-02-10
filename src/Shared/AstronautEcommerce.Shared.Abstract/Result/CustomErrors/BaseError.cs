namespace AstronautEcommerce.Shared.Abstract.Result.CustomErrors;

public sealed record BaseError : Error
{
    public BaseError(string message) : base(message)
    {
    }
}