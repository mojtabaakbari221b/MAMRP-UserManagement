namespace Share.ResponseResult;

public sealed class FailureResponse : ResultResponse
{
    public new List<string>? Errors { get; set; }

    public FailureResponse(string errorMessage, string serviceCode)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
        ServiceCode = serviceCode;
        Errors = null;
    }
    public FailureResponse(List<string> errors, string serviceCode)
    {
        IsSuccess = false;
        ErrorMessage = string.Empty;
        ServiceCode = serviceCode;
        Errors = errors;
    }
}