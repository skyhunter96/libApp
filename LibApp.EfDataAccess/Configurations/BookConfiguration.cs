using Domain.Models;
using EfDataAccess.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDataAccess.Configurations
{
    public class BookConfiguration : BaseEntityConfiguration<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> entity)
        {
            base.Configure(entity);

            entity.Property(e => e.Title)
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            entity.Property(e => e.Isbn)
                .HasColumnType("char(17)");

            entity.Property(e => e.Edition)
                .HasMaxLength(100);

            entity.Property(e => e.ImagePath)
                .HasColumnType("char(255)")
                .IsRequired(false);

            entity.Property(e => e.Cost)
                .HasColumnType("decimal(10,2)")
                .IsRequired(false);

            //TODO: delete behavior should prolly be restrict to all except many-to-many
            entity.HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(b => b.Category)
                .WithMany(p => p.Books)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(b => b.Department)
                .WithMany(p => p.Books)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(b => b.Language)
                .WithMany(p => p.Books)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    r => r.HasOne<Author>().WithMany().HasForeignKey("AuthorId"),
                    l => l.HasOne<Book>().WithMany().HasForeignKey("BookId")
                );
        }
    }
}
