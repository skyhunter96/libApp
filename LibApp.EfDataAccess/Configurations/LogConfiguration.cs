using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDataAccess.Configurations
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> entity)
        {
            entity.HasKey(l => l.Id);

            entity.Property(l => l.Name)
                .HasMaxLength(50);

            entity.Property(l => l.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            entity.Property(l => l.DateTime)
                .HasDefaultValueSql("SYSDATETIME()");

            entity.Property(l => l.StackTrace)
                .HasMaxLength(500)
                .IsRequired(false);

            //TODO: Might not be needed at all
            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
