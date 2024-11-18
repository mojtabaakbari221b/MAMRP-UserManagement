namespace UserManagement.Infrastructure.Persistence.Configuration;

public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder.HasOne(u => u.User)
            .WithMany()
            .HasForeignKey(u => u.UserId);

        var persianRecordDatetimeConverter = new ValueConverter<PersianDateTime, DateTime>(
            p => p.ToDateTime(),
            d => new PersianDateTime(d));

        var persianUpdateDatetimeConverter = new ValueConverter<PersianDateTime, DateTime>(
            p => p.ToDateTime(),
            d => new PersianDateTime(d));

        builder.Property(s => s.PersianRecordDatetime)
            .HasConversion(persianRecordDatetimeConverter);

        builder.Property(s => s.PersianUpdateDatetime)
            .HasConversion(persianUpdateDatetimeConverter);
    }
}