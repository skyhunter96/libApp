using Domain.Models;
using EfDataAccess.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDataAccess.Configurations
{
    public class RateConfiguration : BaseEntityConfiguration<Rate>
    {
        public override void Configure(EntityTypeBuilder<Rate> entity)
        {
            base.Configure(entity);

            entity.Property(r => r.RateFee)
                .HasColumnType("decimal(10,2)");
        }
    }
}
