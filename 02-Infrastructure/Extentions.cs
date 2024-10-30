using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using UserManagement.Infrastructure.Repositories;


public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IMenuRepository, MenuRepository>();
        return services;
    }
}

