using Domain.Models;
using EfDataAccess.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDataAccess.Configurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> entity)
        {
            base.Configure(entity);

            entity.HasMany(u => u.Reservations)
                .WithOne(r => r.ReservedByUser)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
