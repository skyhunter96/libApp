using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDataAccess.Configurations
{
    public class BookReservationConfiguration : IEntityTypeConfiguration<BookReservation>
    {
        public void Configure(EntityTypeBuilder<BookReservation> builder)
        {
            builder.ToTable("BookReservation", "lib");

            builder.HasKey(br => new { br.ReservationId, br.BookId });

            builder.HasOne(br => br.Reservation)
                .WithMany(r => r.BookReservations)
                .HasForeignKey(br => br.ReservationId);

            builder.HasOne(br => br.Book)
                .WithMany(b => b.BookReservations)
                .HasForeignKey(br => br.BookId);

            builder.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("SYSDATETIME()");

            builder.Property(e => e.ModifiedDateTime)
                .HasDefaultValueSql("SYSDATETIME()");
        }
    }
}
