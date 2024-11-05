namespace UserManagement.Infrastructure.Persistence.Configuration;

public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.Ignore(u => u.ClaimType);
        builder.Ignore(u => u.ClaimValue);
    }
}