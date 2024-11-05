namespace UserManagement.Infrastructure.Persistence.Configuration;

public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder.HasOne(u => u.User)
            .WithMany()
            .HasForeignKey(u => u.UserId);
    }
}