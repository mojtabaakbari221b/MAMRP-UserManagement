using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Share.Exceptions.Middleware;

namespace Share;

public static class ConfigureServices
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddScoped<MamrpBaseBadRequestExceptionMiddleware>();
        services.AddScoped<MamrpBaseNotFoundExceptionMiddleware>();
        services.AddScoped<MamrpValidationExceptionMiddleware>();
        return services;
    }

    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        app.UseMiddleware<MamrpBaseBadRequestExceptionMiddleware>();
        app.UseMiddleware<MamrpBaseNotFoundExceptionMiddleware>();
        app.UseMiddleware<MamrpValidationExceptionMiddleware>();
        return app;
    }
}