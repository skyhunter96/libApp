using LibApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibApp.EfDataAccess.Configurations;

public class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> entity)
    {
        entity.ToTable("Languages", "lib");

        entity.HasKey(l => l.Id);

        entity.Property(l => l.Name)
            .HasMaxLength(50);

        entity.HasMany(d => d.Books)
            .WithOne(b => b.Language)
            .OnDelete(DeleteBehavior.Restrict);
    }
}