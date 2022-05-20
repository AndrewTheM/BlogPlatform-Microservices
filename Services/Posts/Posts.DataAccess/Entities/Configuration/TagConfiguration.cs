using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posts.DataAccess.Entities;

namespace Posts.DataAccess.Entities.Configuration;

internal class TagConfiguration : BaseEntityConfiguration<Tag>
{
    public override void Configure(EntityTypeBuilder<Tag> builder)
    {
        base.Configure(builder);

        builder.HasIndex(t => t.TagName)
            .IsUnique();

        builder.Property(t => t.TagName)
            .IsRequired()
            .HasMaxLength(50);
    }
}
