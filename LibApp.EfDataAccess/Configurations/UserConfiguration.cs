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
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Id).ValueGeneratedOnAdd();

            entity.ToTable("User");

            entity.Ignore(u => u.Password);

            entity.Property(u => u.DocumentId)
                .HasMaxLength(13);

            entity.HasIndex(u => u.DocumentId)
                .IsUnique();

            entity.Property(u => u.FirstName)
                .HasMaxLength(50);

            entity.Property(u => u.LastName)
                .HasMaxLength(50);

            entity.Property(u => u.UserName)
                .IsRequired();

            entity.Property(u => u.Email)
                .IsRequired();

            entity.Property(u => u.ImagePath)
                .HasColumnType("char(255)")
                .IsRequired(false);

            entity.Property(u => u.City)
                .HasMaxLength(50);

            entity.Property(u => u.Address)
                .HasMaxLength(100)
                .IsRequired(false);

            entity.Property(u => u.PhoneNumber)
                .HasMaxLength(30)
                .IsRequired(false);

            entity.Property(u => u.TotalFee)
                .HasColumnType("decimal(10,2)");

            entity.Property(u => u.Currency)
                .HasColumnType("char(3)")
                .IsRequired(false);

            entity.Property(u => u.Notes)
                .HasMaxLength(1000)
                .IsRequired(false);

            entity.Property(u => u.CreatedDateTime)
                .HasDefaultValueSql("SYSDATETIME()");

            entity.Property(u => u.ModifiedDateTime)
                .HasDefaultValueSql("SYSDATETIME()");

            entity.HasOne(e => e.CreatedByUser)
                .WithMany()
                .HasForeignKey(e => e.CreatedByUserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(u => u.ModifiedByUser)
                .WithMany()
                .HasForeignKey(u => u.ModifiedByUserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(u => u.Reservations)
                .WithOne(r => r.ReservedByUser)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
