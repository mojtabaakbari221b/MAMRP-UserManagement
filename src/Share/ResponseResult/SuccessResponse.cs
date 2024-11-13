namespace Share.ResponseResult;

public sealed class SuccessResponse : ResultResponse
{
    public SuccessResponse()
    {
        IsSuccess = true;
    }
}

public class SuccessResponse<TValue> : ResultResponse
{
    public TValue? Values { get; private set; }

    public SuccessResponse(TValue values)
    {
        Values = values;
        IsSuccess = true;
    }
}