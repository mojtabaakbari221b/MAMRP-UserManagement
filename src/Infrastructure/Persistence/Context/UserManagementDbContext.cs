namespace UserManagement.Infrastructure.Persistence.Context;

public class UserManagementDbContext(DbContextOptions<UserManagementDbContext> options)
    : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options)
{
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    public DbSet<SectionGroup> SectionGroups { get; set; }
    public DbSet<Section> Sections { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        // optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
        optionsBuilder.AddInterceptors(new FillBaseEntityValuesOnUpdatingInterceptor());
        optionsBuilder.AddInterceptors(new FillBaseEntityValuesOnCreatingInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}