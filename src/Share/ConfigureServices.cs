using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Share.Exceptions.Middleware;

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

    public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
    {
        app.UseSwagger();

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(@"E:\Project\MAMRP-UserManagement\src\Share\SwggerConfig"),
            RequestPath = "/swagger" // مسیری که می‌خواهید فایل‌های استاتیک از آن قابل دسترسی باشند
        });

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");

            // از مسیر فیزیکی فایل جاوااسکریپت استفاده کنید
            c.InjectJavascript(@"E:\Project\MAMRP-UserManagement\src\Share\SwggerConfig");
        });
        return app;
    }
}