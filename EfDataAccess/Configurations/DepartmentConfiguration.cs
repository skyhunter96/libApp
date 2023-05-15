using Domain.Models;
using EfDataAccess.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDataAccess.Configurations
{
    public class DepartmentConfiguration : BaseEntityConfiguration<Department>
    {
        public override void Configure(EntityTypeBuilder<Department> entity)
        {
            base.Configure(entity);

            entity.ToTable("Department", "lib");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            entity.HasMany(d => d.Books)
                .WithOne(b => b.Department)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
