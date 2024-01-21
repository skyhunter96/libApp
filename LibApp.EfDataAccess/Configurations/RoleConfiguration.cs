using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibApp.EfDataAccess.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.ToTable("Role");

            entity.HasKey(e => e.Id);

            entity.Property(r => r.Id).ValueGeneratedNever();

            entity.HasOne(r => r.User)
                .WithOne(u => u.Role)
                .HasForeignKey<User>(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Librarian" },
                new Role { Id = 3, Name = "Regular" }
            );
        }
    }
}
