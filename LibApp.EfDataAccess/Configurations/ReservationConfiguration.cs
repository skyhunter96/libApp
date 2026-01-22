using LibApp.Domain.Models;
using LibApp.EfDataAccess.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibApp.EfDataAccess.Configurations;

public class ReservationConfiguration : BaseEntityConfiguration<Reservation>
{
    public override void Configure(EntityTypeBuilder<Reservation> entity)
    {
        base.Configure(entity);

        entity.Property(reservation => reservation.LoanDate)
            .HasColumnType("date");

        entity.Property(reservation => reservation.DueDate)
            .HasColumnType("date");

        entity.Property(reservation => reservation.ActualReturnDate)
            .HasColumnType("date");

        entity.Property(reservation => reservation.LateFee)
            .HasColumnType("decimal(10,2)");

        entity.HasOne(reservation => reservation.ReservedByUser)
            .WithMany(user => user.Reservations)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}