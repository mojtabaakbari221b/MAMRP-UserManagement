namespace UserManagement.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistance(configuration)
            .AddExternalServices(configuration);

        return services;
    }
}