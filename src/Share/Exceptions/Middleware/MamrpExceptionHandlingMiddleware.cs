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
                return new { IsSuccess = false, IsFailed = true, Message = badRequestEx.Message };

            case MamrpBaseNotFoundException notFoundEx:
                statusCode = (int)HttpStatusCode.NotFound;
                return new { IsSuccess = false, IsFailed = true, Message = notFoundEx.Message };

            case MamrpValidationException validationEx:
                statusCode = (int)HttpStatusCode.UnprocessableEntity;
                return CreateValidationErrorResponse(validationEx);

            default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                return new { IsFailed = true, Message = "An unexpected error occurred." };
        }
    }

    private object CreateValidationErrorResponse(MamrpValidationException ex)
    {
        var validationResult = Result.Fail("Validation Error")
            .WithErrors(ex.Errors
                .SelectMany(e => e.Value.Select(msg => new Error($"{e.Key}: {msg}"))));

        return new
        {
            IsFailed = validationResult.IsFailed,
            IsSuccess = validationResult.IsSuccess,
            Errors = validationResult.Errors.Select(e => new { Message = e.Message }).ToArray()
        };
    }
}