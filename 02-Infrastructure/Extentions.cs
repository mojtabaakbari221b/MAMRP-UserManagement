using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Domain.Repositories;


public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ISectionRepository, SectionRepository>();
        return services;
    }
}

