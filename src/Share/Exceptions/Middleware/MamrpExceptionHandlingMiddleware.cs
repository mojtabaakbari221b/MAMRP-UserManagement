using Share.ResponseResult;

namespace Share.Exceptions.Middleware;

public class MamrpExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex) when (ex is MamrpBaseBadRequestException
                                       or MamrpBaseNotFoundException
                                       or MamrpValidationException)
        {
            context.Response.Headers.Append("content-type", "application/json");
            var response = CreateErrorResponse(ex, out var statusCode);
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

    private object CreateErrorResponse(Exception ex, out int statusCode)
    {
        switch (ex)
        {
            case MamrpBaseBadRequestException badRequestEx:
                statusCode = (int)HttpStatusCode.BadRequest;
                return Result.Fail(badRequestEx.Message, badRequestEx.ServiceCode);

            case MamrpBaseNotFoundException notFoundEx:
                statusCode = (int)HttpStatusCode.NotFound;
                return Result.Fail(notFoundEx.Message, notFoundEx.ServiceCode);

            case MamrpValidationException validationEx:
                statusCode = (int)HttpStatusCode.UnprocessableEntity;
                return Result.FailValidation(validationEx);

            default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                return Result.Fail("An unexpected error occurred.", ServicesCode.UserManagement);
        }
    }
    
}