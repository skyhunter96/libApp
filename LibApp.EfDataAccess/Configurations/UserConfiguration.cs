using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            //From BaseEntityConfig
            entity.HasKey(e => e.Id);

            entity.Property(u => u.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("SYSDATETIME()");

            entity.Property(e => e.ModifiedDateTime)
                .HasDefaultValueSql("SYSDATETIME()");

            entity.HasOne(e => e.CreatedByUser)
                .WithMany()
                .HasForeignKey(e => e.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.ModifiedByUser)
                .WithMany()
                .HasForeignKey(e => e.ModifiedByUserId)
                .OnDelete(DeleteBehavior.Restrict);


            entity.ToTable("User");

            entity.Property(u => u.FirstName)
                .HasMaxLength(50);

            entity.Property(u => u.LastName)
                .HasMaxLength(50);

            entity.Property(e => e.UserName)
                .HasColumnType("char(50)")
                .IsRequired();

            //entity.Property(e => e.Password)
            //    .HasColumnType("char(100)")
            //    .IsRequired();

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

            entity.Property(u => u.PhoneNumber)
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

            entity.HasOne(u => u.Role)
                .WithOne(r => r.User)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(u => u.Reservations)
                .WithOne(r => r.ReservedByUser)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
