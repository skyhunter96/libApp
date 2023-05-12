using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            //TODO: mozda treba i createdAt i modifiedAt

            entity.HasMany(u => u.Reservations)
                .WithOne(r => r.ReservedByUser)
                .HasForeignKey(r => r.ReservedByUserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
