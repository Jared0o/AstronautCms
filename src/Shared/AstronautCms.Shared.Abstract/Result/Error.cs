namespace AstronautCms.Shared.Abstract.Result;

public abstract record Error
{
    public string Message { get; }

    protected Error(string message)
    {
        Message = message;
    }
    public override string ToString() => Message;
}