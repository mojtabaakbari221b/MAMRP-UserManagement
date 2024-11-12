namespace Share.ResponseResult;

public static class Result
{
    public static SuccessResponse Ok()
    {
        return new SuccessResponse();
    }

    public static SuccessResponse<TValue> Ok<TValue>(TValue value)
    {
        return new SuccessResponse<TValue>(value);
    }

    public static FailureResponse Fail(string message, string serviceCode)
    {
        return new FailureResponse(message, serviceCode);
    }

    public static FailureValidationRespobse FailValidation(MamrpValidationException ex)
    {
        return new FailureValidationRespobse(ex.Errors, ex.ServiceCode);
    }
}