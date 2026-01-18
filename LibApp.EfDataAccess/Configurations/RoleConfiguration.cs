using LibApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibApp.EfDataAccess.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> entity)
    {
        entity.ToTable("Role");

        entity.HasKey(r => r.Id);

        entity.Property(r => r.Id).ValueGeneratedNever();

        entity.HasMany(r => r.Users)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasData(
            new Role { Id = 1, Name = RoleEnum.Admin.ToString() },
            new Role { Id = 2, Name = RoleEnum.Librarian.ToString() },
            new Role { Id = 3, Name = RoleEnum.Regular.ToString() }
        );
    }
}