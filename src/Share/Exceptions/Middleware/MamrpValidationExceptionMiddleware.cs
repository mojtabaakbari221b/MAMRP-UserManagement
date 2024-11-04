using System.Net;
using System.Text.Json;
using FluentResults;
using Microsoft.AspNetCore.Http;

namespace Share.Exceptions.Middleware;

public class MamrpValidationExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (MamrpValidationException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
            context.Response.Headers.Append("content-type", "application/json");
            var result = Result.Fail("Validation Error")
                .WithErrors(ex.Errors
                    .SelectMany(e => e.Value.Select(msg => new Error($"{e.Key}: {msg}"))));

            await context.Response.WriteAsync(JsonSerializer.Serialize(result));

            // var response = new
            // {
            //     IsFailed = result.IsFailed,
            //     IsSuccess = result.IsSuccess,
            //     Errors = result.Errors.Select(e => new
            //     {
            //         Message = e.Message
            //     }).ToArray()
            // };
            // await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}