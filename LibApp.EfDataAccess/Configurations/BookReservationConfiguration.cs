using LibApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibApp.EfDataAccess.Configurations;

public class BookReservationConfiguration : IEntityTypeConfiguration<BookReservation>
{
    public void Configure(EntityTypeBuilder<BookReservation> entity)
    {
        entity.ToTable("BookReservations", "lib");

        entity.HasKey(bookReservation => new { bookReservation.ReservationId, bookReservation.BookId });

        entity.HasOne(bookReservation => bookReservation.Reservation)
            .WithMany(reservation => reservation.BookReservations)
            .HasForeignKey(bookReservation => bookReservation.ReservationId);

        entity.HasOne(bookReservation => bookReservation.Book)
            .WithMany(book => book.BookReservations)
            .HasForeignKey(bookReservation => bookReservation.BookId);

        entity.Property(bookReservation => bookReservation.CreatedDateTime)
            .HasDefaultValueSql("SYSDATETIME()");

        entity.Property(bookReservation => bookReservation.ModifiedDateTime)
            .HasDefaultValueSql("SYSDATETIME()");
    }
}