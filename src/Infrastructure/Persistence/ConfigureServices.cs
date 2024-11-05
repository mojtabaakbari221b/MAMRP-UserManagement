namespace UserManagement.Infrastructure.Persistence;

public static class ConfigureServices
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        // DI Repository
        services.AddScoped<ISectionGroupRepository, SectionGroupRepository>();
        services.AddScoped<ISectionRepository, SectionRepository>();
        // DI UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // DI DbContext 
        services.AddDbContext<UserManagementDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        // DI Identity
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<UserManagementDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}