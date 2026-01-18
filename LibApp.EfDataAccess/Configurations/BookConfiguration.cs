using LibApp.Domain.Models;
using LibApp.EfDataAccess.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibApp.EfDataAccess.Configurations;

public class BookConfiguration : BaseEntityConfiguration<Book>
{
    public override void Configure(EntityTypeBuilder<Book> entity)
    {
        base.Configure(entity);

        entity.Property(b => b.Title)
            .HasMaxLength(100);

        entity.Property(b => b.Description)
            .HasMaxLength(1000)
            .IsRequired(false);

        entity.Property(b => b.Isbn)
            .HasColumnType("char(17)");

        entity.Property(b => b.Edition)
            .HasMaxLength(100);

        entity.Property(b => b.ImagePath)
            .IsRequired(false);

        entity.Property(b => b.Cost)
            .HasColumnType("decimal(10,2)")
            .IsRequired(false);

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