namespace Share.ResponseResult;

public sealed class FailureValidationRespobse : ResultResponse
{
    public FailureValidationRespobse(IDictionary<string, string[]> errors, string serviceCode)
    {
        Errors = errors;
        ServiceCode = serviceCode;
        IsSuccess = false;
    }

    public IDictionary<string, string[]> Errors { get; private set; }
    public string ServiceCode { get; private set; }
}