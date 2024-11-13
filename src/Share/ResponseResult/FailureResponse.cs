namespace Share.ResponseResult;

public sealed class FailureResponse : ResultResponse
{
    public FailureResponse(string errorMessage, string serviceCode)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
        ServiceCode = serviceCode;
    }
}