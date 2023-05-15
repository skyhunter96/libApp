using Domain.Models;
using EfDataAccess.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDataAccess.Configurations
{
    public class AuthorConfiguration : BaseEntityConfiguration<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> entity)
        {
            base.Configure(entity);

            entity.ToTable("Authors", "lib");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
