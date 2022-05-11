using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPlatform.Posts.DataAccess.Entities.Configuration;

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
