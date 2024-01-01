using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDataAccess.Configurations.Common
{
    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> entity)
        {
            var entityType = typeof(TEntity);
            var tableName = entityType.Name;
            entity.ToTable(tableName, "lib");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("SYSDATETIME()");

            entity.Property(e => e.ModifiedDateTime)
                .HasDefaultValueSql("SYSDATETIME()");

            entity.HasOne(e => e.CreatedByUser)
                .WithMany()
                .HasForeignKey(e => e.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.ModifiedByUser)
                .WithMany()
                .HasForeignKey(e => e.ModifiedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
