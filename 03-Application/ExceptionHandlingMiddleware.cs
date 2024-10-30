using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, ValidationException exception)
    {

        var response = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "ValidationFailure",
            Title = "Validation error",
            Detail = "One or more validation errors has occurred"
        };

        if (exception.Errors is not null)
        {
            response.Extensions["errors"] = exception.Errors;
        }

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

}