using LibApp.Domain.Models;
using LibApp.EfDataAccess.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibApp.EfDataAccess.Configurations;

public class DepartmentConfiguration : BaseEntityConfiguration<Department>
{
    public override void Configure(EntityTypeBuilder<Department> entity)
    {
        base.Configure(entity);

        entity.Property(department => department.Name)
            .HasMaxLength(50);

        entity.Property(department => department.Description)
            .HasMaxLength(1000)
            .IsRequired(false);

        entity.Property(department => department.Location)
            .HasMaxLength(100)
            .IsRequired(false);

        entity.Property(department => department.Budget)
            .HasColumnType("decimal(10,2)");

        entity.HasMany(department => department.Books)
            .WithOne(book => book.Department)
            .OnDelete(DeleteBehavior.Restrict);
    }
}