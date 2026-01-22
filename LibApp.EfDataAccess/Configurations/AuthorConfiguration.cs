using LibApp.Domain.Models;
using LibApp.EfDataAccess.Configurations.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibApp.EfDataAccess.Configurations;

public class AuthorConfiguration : BaseEntityConfiguration<Author>
{
    public override void Configure(EntityTypeBuilder<Author> entity)
    {
        base.Configure(entity);

        entity.Property(author => author.Name)
            .HasMaxLength(100);
    }
}