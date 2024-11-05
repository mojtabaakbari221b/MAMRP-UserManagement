namespace UserManagement.Infrastructure.ExternalServices;

public static class ConfigureServices
{
    internal static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
    {
        // DI Options
        services.Configure<TokenOption>(configuration);
        return services;
    }
}