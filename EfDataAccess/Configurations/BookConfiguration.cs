using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDataAccess.Configurations
{
    public class BookConfiguration : BaseEntityConfiguration<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> entity)
        {
            base.Configure(entity);

            entity.ToTable("Books", "lib");

            entity.Property(e => e.Title)
                .HasMaxLength(100);

            entity.Property(e => e.Cost)
                .HasColumnType("decimal(10,2)");

            entity.Property(e => e.Description)
                .HasMaxLength(1000);

            entity.Property(e => e.Edition)
                .HasMaxLength(100);

            entity.Property(e => e.Isbn)
                .HasColumnType("char(50)");
        }
    }
}
