using Domain.Models;
using EfDataAccess.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDataAccess.Configurations
{
    public class PublisherConfiguration : BaseEntityConfiguration<Publisher>
    {
        public override void Configure(EntityTypeBuilder<Publisher> entity)
        {
            base.Configure(entity);

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsRequired(false);

            //TODO: Check behaviour
            entity.HasMany(p => p.Books)
                .WithOne(b => b.Publisher)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
