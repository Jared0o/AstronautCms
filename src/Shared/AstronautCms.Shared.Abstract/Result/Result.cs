namespace AstronautCms.Shared.Abstract.Result;

public class Result<T>
{
    public bool IsSuccess { get; }
    public Error? Error { get; }
    public T? Value { get; }

    protected Result(T value)
    {
        IsSuccess = true;
        Value = value;
    }

    protected Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static Result<T> Success(T value) => new(value);

    public static Result<T> Failure(Error error) => new(error);
    
    public T GetValueOrThrow()
    {
        if (!IsSuccess)
            throw new InvalidOperationException("Result is failure.");
        return Value ?? throw new InvalidOperationException("Result has no value.");
    }
}

public class Result
{
    public bool IsSuccess { get; }
    public Error? Error { get; }

    protected Result(bool isSuccess, Error? error = null)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true);

    public static Result Failure(Error error) => new(false, error);
}