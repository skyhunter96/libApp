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

            entity.Property(e => e.Name)
                .HasMaxLength(50);

            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsRequired(false);

            entity.Property(e => e.Budget)
                .HasColumnType("decimal(10,2)");

            entity.HasMany(d => d.Books)
                .WithOne(b => b.Department)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.ParentDepartment)
                .WithMany(d => d.ChildDepartments)
                .HasForeignKey(d => d.ParentDepartmentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
