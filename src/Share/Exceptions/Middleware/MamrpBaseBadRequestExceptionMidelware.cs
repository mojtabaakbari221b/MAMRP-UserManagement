using System.Net;
using System.Text.Json;
using FluentResults;
using Microsoft.AspNetCore.Http;

namespace Share.Exceptions.Middleware;

public class MamrpBaseBadRequestExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (MamrpBaseBadRequestException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.Headers.Append("content-type", "application/json");
            var result = Result.Fail(ex.Message);
            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
}