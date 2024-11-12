namespace Share.ResponseResult;

public sealed class FailureResponse : ResultResponse
{
    public string ErrorMessage { get; private set; }
    public string ServiceCode { get; private set; }

    public FailureResponse(string errorMessage, string serviceCode)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
        ServiceCode = serviceCode;
    }
}