namespace Share.ResponseResult;

public abstract class ResultResponse
{
    protected bool IsSuccess { get; init; }
    public bool IsFailed => !IsSuccess;
}