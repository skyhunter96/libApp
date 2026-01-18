using LibApp.Domain.Models;
using LibApp.EfDataAccess.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibApp.EfDataAccess.Configurations;

public class PublisherConfiguration : BaseEntityConfiguration<Publisher>
{
    public override void Configure(EntityTypeBuilder<Publisher> entity)
    {
        base.Configure(entity);

        entity.Property(p => p.Name)
            .HasMaxLength(50);

        entity.Property(p => p.Description)
            .HasMaxLength(100)
            .IsRequired(false);

        entity.HasMany(p => p.Books)
            .WithOne(b => b.Publisher)
            .OnDelete(DeleteBehavior.Restrict);
    }
}