using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentResults;
using FluentValidation.Results;

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

        //Result result = new Result();

        if (exception.Errors is not null)
        {
            //Error error = new Error();
            var result = Result.Ok(exception.Errors);
            //response.Extensions["errors"] = exception.Errors;
            //await httpContext.Response.WriteAsync(JsonSerializer.Serialize(result));
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(result));
        }

        //httpContext.Response.ContentType = "application/json";

        //httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        //await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

}