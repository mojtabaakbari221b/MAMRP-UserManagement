using UserManagement.Infrastructure.ExternalServices.Identities.Managers;
using UserManagement.Infrastructure.ExternalServices.Identities.Tokens;

namespace UserManagement.Infrastructure.ExternalServices;

public static class ConfigureServices
{
    internal static IServiceCollection AddExternalServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // DI Identities
        services.AddScoped<ITokenFactory, TokenFactory>();
        services.AddScoped<IRoleManager, RoleManager>();
        services.AddScoped<IUserManager, UserManager>();

        // DI Options
        services.Configure<TokenOption>(configuration);

        services.AddIdentityConfiguration(configuration);
        
        return services;
    }
}