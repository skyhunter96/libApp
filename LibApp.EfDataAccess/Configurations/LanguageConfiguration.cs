using LibApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibApp.EfDataAccess.Configurations;

public class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> entity)
    {
        entity.ToTable("Languages", "lib");

        entity.HasKey(language => language.Id);

        entity.Property(language => language.Name)
            .HasMaxLength(50);

        entity.HasMany(department => department.Books)
            .WithOne(book => book.Language)
            .OnDelete(DeleteBehavior.Restrict);
    }
}