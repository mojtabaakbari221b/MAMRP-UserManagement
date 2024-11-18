namespace UserManagement.Infrastructure.Persistence.Context;

public class UserManagementDbContext(
    DbContextOptions<UserManagementDbContext> options,
    FillBaseEntityValuesOnUpdatingInterceptor onUpdatingInterceptor,
    FillBaseEntityValuesOnCreatingInterceptor onCreatingInterceptor,
    SoftDeleteInterceptor softDeleteInterceptor
    ) : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options)
{
    private readonly FillBaseEntityValuesOnCreatingInterceptor _onCreatingInterceptor = onCreatingInterceptor;
    private readonly FillBaseEntityValuesOnUpdatingInterceptor _onUpdatingInterceptor = onUpdatingInterceptor;
    private readonly SoftDeleteInterceptor _softDeleteInterceptor = softDeleteInterceptor;
    
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    public DbSet<SectionGroup> SectionGroups { get; set; }
    public DbSet<Section> Sections { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors(_softDeleteInterceptor, _onCreatingInterceptor, _onUpdatingInterceptor);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ConfigSoftDeleteFilter();
    }
}