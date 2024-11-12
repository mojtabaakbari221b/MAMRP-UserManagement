using UserManagement.Infrastructure.ExternalServices.Identities.Helper;

namespace UserManagement.Infrastructure.Persistence.Context;

public class UserManagementDbContext(DbContextOptions<UserManagementDbContext> options)
    : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options)
{
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    public DbSet<SectionGroup> SectionGroups { get; set; }
    public DbSet<Section> Sections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        SeedingData(modelBuilder);
    }

    private void SeedingData(ModelBuilder modelBuilder)
    {
        var declaratedServices = ConstantRetriever.GetConstants(typeof(ServiceDeclaration));
        foreach (var service in declaratedServices)
        {
            modelBuilder.Entity<Section>()
                .HasData(
                    new Section
                    {
                        Name = service.Name,
                        Url = $"/{service.Name}",
                        Description = service.Name,
                        Code = service.Value,
                        Type = SectionType.Service,
                    }
                );
        }
    }
}