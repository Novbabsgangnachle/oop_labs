namespace DefaultNamespace;

public abstract record OperationResult
{
    private OperationResult() { }

    public sealed record Success : OperationResult { }

    public sealed record Failure(string Message) : OperationResult;
}