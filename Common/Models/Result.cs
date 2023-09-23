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
    
    public static implicit operator bool(Result<T> result)
    {
        return result.IsSuccess();
    }
    
    public static implicit operator Result<T>(bool value)
    {
        if (value == true)
            throw new ArgumentException();
        
        return new Result<T>();
    }
}