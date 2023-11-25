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

            entity.ToTable("User");

            entity.Property(u => u.FirstName)
                .HasMaxLength(50);

            entity.Property(u => u.LastName)
                .HasMaxLength(50);

            entity.Property(e => e.Username)
                .HasColumnType("char(50)")
                .IsRequired();

            entity.Property(e => e.Password)
                .HasColumnType("char(100)")
                .IsRequired();

            entity.Property(e => e.Email)
                .HasColumnType("char(50)")
                .IsRequired();

            entity.Property(e => e.VerificationToken)
                .HasColumnType("char(128)")
                .IsRequired(false);

            entity.Property(u => u.VerificationSentAt)
                .IsRequired(false);

            entity.Property(e => e.ImagePath)
                .HasColumnType("char(255)")
                .IsRequired(false);

            entity.Property(u => u.City)
                .HasMaxLength(50);

            entity.Property(u => u.Address)
                .HasMaxLength(100)
                .IsRequired(false);

            entity.Property(u => u.Phone)
                .HasColumnType("char(30)")
                .IsRequired(false);

            entity.Property(e => e.TotalFee)
                .HasColumnType("decimal(10,2)");

            entity.Property(u => u.Currency)
                .HasColumnType("char(3)")
                .IsRequired(false);

            entity.Property(u => u.Notes)
                .HasMaxLength(1000)
                .IsRequired(false);

            entity.Property(u => u.RoleId)
                .HasColumnName("RoleId")
                .IsRequired();

            entity.Ignore(u => u.Role);

            entity.HasMany(u => u.Reservations)
                .WithOne(r => r.ReservedByUser)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
