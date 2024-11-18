namespace UserManagement.Infrastructure.Persistence.Configuration;

public static class SoftDeleteConfiguration
{
    public static void ConfigSoftDeleteFilter(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasQueryFilter(f => f.IsActive);
        modelBuilder.Entity<RoleClaim>().HasQueryFilter(f => f.IsActive);
        modelBuilder.Entity<User>().HasQueryFilter(f => f.IsActive);
        modelBuilder.Entity<UserClaim>().HasQueryFilter(f => f.IsActive);
        modelBuilder.Entity<UserLogin>().HasQueryFilter(f => f.IsActive);
        modelBuilder.Entity<UserRefreshToken>().HasQueryFilter(f => f.IsActive);
        modelBuilder.Entity<UserRole>().HasQueryFilter(f => f.IsActive);
        modelBuilder.Entity<UserToken>().HasQueryFilter(f => f.IsActive);
        modelBuilder.Entity<SectionGroup>().HasQueryFilter(f => f.IsActive);
        modelBuilder.Entity<Section>().HasQueryFilter(f => f.IsActive);
    }
}