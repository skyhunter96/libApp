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

            entity.Property(c => c.Name)
                .HasMaxLength(50);

            entity.Property(c => c.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            entity.HasMany(c => c.Books)
                .WithOne(b => b.Category)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
