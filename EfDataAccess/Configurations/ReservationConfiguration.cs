using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDataAccess.Configurations
{
    public class ReservationConfiguration : BaseEntityConfiguration<Reservation>
    {
        public override void Configure(EntityTypeBuilder<Reservation> entity)
        {
            base.Configure(entity);

            entity.ToTable("Reservations", "lib");

            //TODO: Check if good behaviour

            //TODO: mozda restrict
            entity.HasOne(r => r.ReservedByUser)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.ReservedByUserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
