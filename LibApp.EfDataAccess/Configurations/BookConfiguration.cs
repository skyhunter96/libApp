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

        entity.Property(book => book.Title)
            .HasMaxLength(100);

        entity.Property(book => book.Description)
            .HasMaxLength(1000)
            .IsRequired(false);

        entity.Property(book => book.Isbn)
            .HasColumnType("char(17)");

        entity.Property(book => book.Edition)
            .HasMaxLength(100);

        entity.Property(book => book.ImagePath)
            .IsRequired(false);

        entity.Property(book => book.Cost)
            .HasColumnType("decimal(10,2)")
            .IsRequired(false);

        entity.HasOne(book => book.Publisher)
            .WithMany(publisher => publisher.Books)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(book => book.Category)
            .WithMany(category => category.Books)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(book => book.Department)
            .WithMany(department => department.Books)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(book => book.Language)
            .WithMany(language => language.Books)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(book => book.Authors)
            .WithMany(author => author.Books)
            .UsingEntity<Dictionary<string, object>>(
                "BookAuthor",
                r => r.HasOne<Author>().WithMany().HasForeignKey("AuthorId"),
                l => l.HasOne<Book>().WithMany().HasForeignKey("BookId")
            );
    }
}