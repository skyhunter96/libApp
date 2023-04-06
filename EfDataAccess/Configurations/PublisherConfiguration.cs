using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDataAccess.Configurations
{
    public class PublisherConfiguration : BaseEntityConfiguration<Publisher>
    {
        public override void Configure(EntityTypeBuilder<Publisher> entity)
        {
            base.Configure(entity);

            entity.ToTable("Publishers", "lib");

            entity.Property(e => e.Name)
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasMaxLength(100);
        }
    }
}
