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

            entity.Property(d => d.Name)
                .HasMaxLength(50);

            entity.Property(d => d.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            entity.Property(d => d.Location)
                .HasMaxLength(100)
                .IsRequired(false);

            entity.Property(d => d.Budget)
                .HasColumnType("decimal(10,2)");

            entity.HasMany(d => d.Books)
                .WithOne(b => b.Department)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
