using LibApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibApp.EfDataAccess.Configurations;

public class BookReservationConfiguration : IEntityTypeConfiguration<BookReservation>
{
    public void Configure(EntityTypeBuilder<BookReservation> entity)
    {
        entity.ToTable("BookReservations", "lib");

        entity.HasKey(br => new { br.ReservationId, br.BookId });

        entity.HasOne(br => br.Reservation)
            .WithMany(r => r.BookReservations)
            .HasForeignKey(br => br.ReservationId);

        entity.HasOne(br => br.Book)
            .WithMany(b => b.BookReservations)
            .HasForeignKey(br => br.BookId);

        entity.Property(br => br.CreatedDateTime)
            .HasDefaultValueSql("SYSDATETIME()");

        entity.Property(br => br.ModifiedDateTime)
            .HasDefaultValueSql("SYSDATETIME()");
    }
}