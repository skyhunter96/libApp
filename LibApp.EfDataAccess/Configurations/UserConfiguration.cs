using LibApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibApp.EfDataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        //From BaseEntityConfig
        entity.HasKey(user => user.Id);

        entity.Property(user => user.Id).ValueGeneratedOnAdd();

        entity.ToTable("User");

        entity.Ignore(user => user.Password);

        entity.Property(user => user.DocumentId)
            .HasMaxLength(13);

        entity.HasIndex(user => user.DocumentId)
            .IsUnique();

        entity.Property(user => user.FirstName)
            .HasMaxLength(50);

        entity.Property(user => user.LastName)
            .HasMaxLength(50);

        entity.Property(user => user.UserName)
            .IsRequired();

        entity.Property(user => user.Email)
            .IsRequired();

        entity.Property(user => user.ImagePath)
            .HasColumnType("char(255)")
            .IsRequired(false);

        entity.Property(user => user.City)
            .HasMaxLength(50);

        entity.Property(user => user.Address)
            .HasMaxLength(100)
            .IsRequired(false);

        entity.Property(user => user.PhoneNumber)
            .HasMaxLength(30)
            .IsRequired(false);

        entity.Property(user => user.TotalFee)
            .HasColumnType("decimal(10,2)");

        entity.Property(user => user.Currency)
            .HasColumnType("char(3)")
            .IsRequired(false);

        entity.Property(user => user.Notes)
            .HasMaxLength(1000)
            .IsRequired(false);

        entity.Property(user => user.CreatedDateTime)
            .HasDefaultValueSql("SYSDATETIME()");

        entity.Property(user => user.ModifiedDateTime)
            .HasDefaultValueSql("SYSDATETIME()");

        entity.HasOne(user => user.CreatedByUser)
            .WithMany()
            .HasForeignKey(user => user.CreatedByUserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(user => user.ModifiedByUser)
            .WithMany()
            .HasForeignKey(user => user.ModifiedByUserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(u => u.Role)
            .WithMany(role => role.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(user => user.Reservations)
            .WithOne(reservation => reservation.ReservedByUser)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}