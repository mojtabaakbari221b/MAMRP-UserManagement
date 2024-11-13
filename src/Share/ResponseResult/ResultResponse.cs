namespace Share.ResponseResult;

public abstract class ResultResponse
{
    public bool IsSuccess { get; protected set; }
    public bool IsFailed => !IsSuccess;
}