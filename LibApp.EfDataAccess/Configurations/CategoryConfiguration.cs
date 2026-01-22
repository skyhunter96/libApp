using LibApp.Domain.Models;
using LibApp.EfDataAccess.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibApp.EfDataAccess.Configurations;

public class CategoryConfiguration : BaseEntityConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> entity)
    {
        base.Configure(entity);

        entity.Property(category => category.Name)
            .HasMaxLength(50);

        entity.Property(category => category.Description)
            .HasMaxLength(1000)
            .IsRequired(false);

        entity.HasMany(category => category.Books)
            .WithOne(book => book.Category)
            .OnDelete(DeleteBehavior.Restrict);
    }
}