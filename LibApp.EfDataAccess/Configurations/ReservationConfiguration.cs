using Domain.Models;
using EfDataAccess.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDataAccess.Configurations
{
    public class ReservationConfiguration : BaseEntityConfiguration<Reservation>
    {
        public override void Configure(EntityTypeBuilder<Reservation> entity)
        {
            base.Configure(entity);

            entity.Property(r => r.LoanDate)
                .HasColumnType("date");

            entity.Property(r => r.DueDate)
                .HasColumnType("date");

            entity.Property(r => r.ActualReturnDate)
                .HasColumnType("date");

            entity.Property(e => e.LateFee)
                .HasColumnType("decimal(10,2)");

            //TODO: Check if good behaviour
            //TODO: mozda restrict onDelete
            entity.HasOne(r => r.ReservedByUser)
                .WithMany(u => u.Reservations)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
