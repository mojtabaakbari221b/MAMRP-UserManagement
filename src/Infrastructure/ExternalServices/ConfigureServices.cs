namespace UserManagement.Infrastructure.ExternalServices;

public static class ConfigureServices
{
    internal static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
    {
        // DI Identities
        services.AddScoped<IAccountManager, AccountManager>();
        services.AddScoped<ITokenFactory, TokenFactory>();

        // DI Options
        services.Configure<TokenOption>(configuration);
        return services;
    }
}