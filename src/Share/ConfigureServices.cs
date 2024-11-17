using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Share.Exceptions.Middleware;
using Share.Extensions;

namespace Share;

public static class ConfigureServices
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddScoped<MamrpExceptionHandlingMiddleware>();
        return services;
    }

    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        app.UseMiddleware<MamrpExceptionHandlingMiddleware>();
        return app;
    }
}