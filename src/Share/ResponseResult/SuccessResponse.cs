namespace Share.ResponseResult;

public sealed class SuccessResponse<TValue> : ResultResponse
{
    public TValue Value { get; private set; }

    public SuccessResponse(TValue value)
    {
        IsSuccess = true;
        Value = value;
    }
}

public sealed class SuccessResponse : ResultResponse
{
    public SuccessResponse()
    {
        IsSuccess = true;
    }
}