using Domain.Models;
using EfDataAccess.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDataAccess.Configurations
{
    public class CategoryConfiguration : BaseEntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> entity)
        {
            base.Configure(entity);

            entity.ToTable("Categories", "lib");

            entity.Property(e => e.Name)
                .HasMaxLength(50);

            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            entity.HasMany(c => c.Books)
                .WithOne(b => b.Category)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
