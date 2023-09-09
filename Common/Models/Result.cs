namespace Common.Models;

public struct Result<T>
{
    public readonly ResultStatusCode ResultStatus;
    public readonly T Value;

    public Result(T value)
    {
        ResultStatus = ResultStatusCode.Success;
        Value = value;
    }
    
    public Result()
    {
        ResultStatus = ResultStatusCode.Failure;
        Value = default;
    }

    public bool IsSuccess() => ResultStatus == ResultStatusCode.Success;
    public bool IsFailure() => ResultStatus == ResultStatusCode.Failure;
}

public enum ResultStatusCode
{
    Success,
    Failure
}