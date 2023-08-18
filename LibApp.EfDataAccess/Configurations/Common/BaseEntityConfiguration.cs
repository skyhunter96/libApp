using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDataAccess.Configurations.Common
{
    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            var entityType = typeof(TEntity);
            var tableName = entityType.Name;
            builder.ToTable(tableName, "lib");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("SYSDATETIME()");

            builder.Property(e => e.ModifiedDateTime)
                .HasDefaultValueSql("SYSDATETIME()");
        }
    }
}
