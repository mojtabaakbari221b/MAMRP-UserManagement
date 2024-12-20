﻿namespace UserManagement.Infrastructure.Persistence.Configuration;

public class SectionConfiguration : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.ToTable("Sections");

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Url)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(s => s.Type)
            .HasConversion<int>()
            .IsRequired();

        builder.HasOne(s => s.Group)
            .WithMany()
            .HasForeignKey(s => s.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();

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