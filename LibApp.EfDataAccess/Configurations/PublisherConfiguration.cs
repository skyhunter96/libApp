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

        entity.Property(publisher => publisher.Name)
            .HasMaxLength(50);

        entity.Property(publisher => publisher.Description)
            .HasMaxLength(100)
            .IsRequired(false);

        entity.HasMany(publisher => publisher.Books)
            .WithOne(book => book.Publisher)
            .OnDelete(DeleteBehavior.Restrict);
    }
}