using LibApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibApp.EfDataAccess.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> entity)
    {
        entity.ToTable("Role");

        entity.HasKey(role => role.Id);

        entity.Property(role => role.Id).ValueGeneratedNever();

        entity.HasMany(role => role.Users)
            .WithOne(user => user.Role)
            .HasForeignKey(user => user.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasData(
            new Role { Id = 1, Name = nameof(RoleEnum.Admin) },
            new Role { Id = 2, Name = nameof(RoleEnum.Librarian) },
            new Role { Id = 3, Name = nameof(RoleEnum.Regular) }
        );
    }
}