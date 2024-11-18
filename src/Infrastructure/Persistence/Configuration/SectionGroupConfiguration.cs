namespace UserManagement.Infrastructure.Persistence.Configuration;

public class SectionGroupConfiguration : IEntityTypeConfiguration<SectionGroup>
{
    public void Configure(EntityTypeBuilder<SectionGroup> builder)
    {
        builder.ToTable("SectionGroups");

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100); 

        builder.Property(e => e.Type)
            .HasConversion<int>() 
            .IsRequired();
        
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
