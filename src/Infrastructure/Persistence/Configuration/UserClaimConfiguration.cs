namespace UserManagement.Infrastructure.Persistence.Configuration;

public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.Ignore(u => u.ClaimType);
        builder.Ignore(u => u.ClaimValue);
    }
}