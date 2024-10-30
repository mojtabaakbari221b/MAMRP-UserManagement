using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UserManagement.Application.Queries.Handlers;


public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddMenuCommandHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllMenuQueryHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetMenuByIdQueryHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteMenuCommandHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateMenuCommandHandler).Assembly));

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient<ExceptionHandlingMiddleware>();

        return services;
        
    }

    public static IApplicationBuilder UseApplication(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        return app;
    }

}

