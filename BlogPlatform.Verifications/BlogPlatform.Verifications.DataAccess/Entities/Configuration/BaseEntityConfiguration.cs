using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPlatform.Verifications.DataAccess.Entities.Configuration;

internal class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : EntityBase
{
    private const string SqlTimestamp = "CURRENT_TIMESTAMP";

    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
               .HasDefaultValueSql("NEWID()");

        builder.Property(e => e.CreatedOn)
               .HasDefaultValueSql(SqlTimestamp);

        builder.Property(e => e.UpdatedOn)
               .HasDefaultValueSql(SqlTimestamp);
    }
}
