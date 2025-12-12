namespace KePass.Server.Commons;

public readonly struct OperationResult<T>
{
    public bool Success { get; }
    public T Result { get; }
    public string Error { get; }

    private OperationResult(bool success, T? result, string? error)
    {
        Success = success;
        Result = result!;
        Error = error ?? string.Empty;
    }

    public static OperationResult<T> Ok(T data)
    {
        return new OperationResult<T>(true, data, null);
    }

    public static OperationResult<T> Fail(string error)
    {
        return new OperationResult<T>(false, default, error);
    }
}