﻿namespace UserManagement.Infrastructure.Persistence;

public static class ConfigureServices
{
    public static WebApplication UseSeedingData(this WebApplication app)
    {
        var seedDataSection = app.Services.GetService<IServiceSeedData>();
        seedDataSection!.SeedData();

        return app;
    }

    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        // Registering IHttpContextAccessor for DI
        services.AddHttpContextAccessor();

        // DI Repository
        services.AddScoped<ISectionGroupRepository, SectionGroupRepository>();
        services.AddScoped<ISectionRepository, SectionRepository>();

        // DI Interceptor
        services.AddScoped<FillBaseEntityValuesOnUpdatingInterceptor>();
        services.AddScoped<FillBaseEntityValuesOnCreatingInterceptor>();
        services.AddScoped<SoftDeleteInterceptor>();

        // DI UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // DI Seeding Data Service
        services.AddSingleton<IServiceSeedData, ServiceSeedData>();

        // DI DbContext 
        services.AddDbContext<UserManagementDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });


        // DI Identity
        services.AddIdentity<User, Role>(options =>
        {
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            options.User.RequireUniqueEmail = false;
        })
        .AddEntityFrameworkStores<UserManagementDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }
}