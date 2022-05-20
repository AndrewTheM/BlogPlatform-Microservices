using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posts.DataAccess.Entities;

namespace Posts.DataAccess.Entities.Configuration;

internal class PostContentConfiguration : BaseEntityConfiguration<PostContent>
{
    public override void Configure(EntityTypeBuilder<PostContent> builder)
    {
        base.Configure(builder);

        builder.Property(pc => pc.Id)
            .ValueGeneratedNever();

        builder.Property(pc => pc.Content)
            .IsRequired();
    }
}
