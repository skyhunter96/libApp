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

            entity.Property(u => u.CreatedDateTime)
                .HasDefaultValueSql("SYSDATETIME()");

            entity.Property(u => u.ModifiedDateTime)
                .HasDefaultValueSql("SYSDATETIME()");

            entity.HasOne(e => e.CreatedByUser)
                .WithMany()
                .HasForeignKey(e => e.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(u => u.ModifiedByUser)
                .WithMany()
                .HasForeignKey(u => u.ModifiedByUserId)
                .OnDelete(DeleteBehavior.Restrict);


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
                .HasColumnType("char(50)")
                .IsRequired();

            entity.Property(u => u.Email)
                .HasColumnType("char(50)");

            entity.Property(u => u.VerificationToken)
                .HasColumnType("char(128)")
                .IsRequired(false);

            entity.Property(u => u.VerificationSentAt)
                .IsRequired(false);

            entity.Property(u => u.ImagePath)
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

            entity.Property(u => u.TotalFee)
                .HasColumnType("decimal(10,2)");

            entity.Property(u => u.Currency)
                .HasColumnType("char(3)")
                .IsRequired(false);

            entity.Property(u => u.Notes)
                .HasMaxLength(1000)
                .IsRequired(false);

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
