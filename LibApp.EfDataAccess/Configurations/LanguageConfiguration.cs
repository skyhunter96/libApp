using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDataAccess.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> entity)
        {
            entity.ToTable("Languages", "lib");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

            entity.HasMany(d => d.Books)
                .WithOne(b => b.Language)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
