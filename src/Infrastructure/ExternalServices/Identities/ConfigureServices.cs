namespace UserManagement.Infrastructure.ExternalServices.Identities;

public static class ConfigureServices
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var optionBearer = configuration.GetSection("BearerTokenOption").Get<BearerTokenOption>()!;
        var optionRefresh = configuration.GetSection("RefreshTokenOption").Get<RefreshTokenOption>()!;
        
        services.AddAuthorizationBuilder()
            .AddPolicy(SectionCode.MamRp01000, policy => policy.RequireRole(SectionCode.MamRp01000))
            .AddPolicy(SectionCode.MamRp02000, policy => policy.RequireRole(SectionCode.MamRp02000));

        // Needed for jwt auth.
        services.AddAuthentication(options =>
        {
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = optionBearer.Issuer, // site that makes the token
                    ValidateIssuer = true,
                    ValidAudience = optionBearer.Audience, // site that consumes the token
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(optionBearer.Key)),
                    ValidateIssuerSigningKey = true, // verify signature to avoid tampering
                    ValidateLifetime = true, // validate the expiration
                    ClockSkew = TimeSpan.Zero // tolerance for the expiration date
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>()
                            .CreateLogger(nameof(JwtBearerEvents));
                        logger.LogError("Authentication failed. Exception:{}", context.Exception);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        return Task.CompletedTask;
                        // var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<IBearerTokenValidatorService>();
                        // return tokenValidatorService.ValidateAsync(context);
                    },
                    OnMessageReceived = context => { return Task.CompletedTask; },
                    OnChallenge = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>()
                            .CreateLogger(nameof(JwtBearerEvents));
                        logger.LogError("OnChallenge error Exception:{}, Description:{}", context.Error,
                            context.ErrorDescription);
                        return Task.CompletedTask;
                    }
                };
            }).AddJwtBearer("RefershSchema" , options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = optionRefresh.Issuer, // site that makes the token
                    ValidateIssuer = true,
                    ValidAudience = optionRefresh.Audience, // site that consumes the token
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(optionRefresh.Key)),
                    ValidateIssuerSigningKey = true, // verify signature to avoid tampering
                    ValidateLifetime = true, // validate the expiration
                    ClockSkew = TimeSpan.Zero // tolerance for the expiration date
                };
            });
        return services;
    }
}