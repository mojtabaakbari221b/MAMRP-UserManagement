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
    }
}
