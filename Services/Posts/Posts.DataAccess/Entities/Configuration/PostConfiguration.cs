using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Posts.DataAccess.Entities.Configuration
{
    internal class PostConfiguration : BaseEntityConfiguration<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Author)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(p => p.Title)
                .IsUnique();

            builder.HasIndex(p => p.TitleIdentifier)
                .IsUnique();

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.TitleIdentifier)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.ThumbnailPath)
                .HasMaxLength(500);

            builder.HasOne(p => p.ContentEntity)
                .WithOne(pc => pc.Post)
                .HasForeignKey<PostContent>(pc => pc.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Tags)
                .WithMany(t => t.Posts)
                .UsingEntity(j => j.ToTable("TagsOfPosts"));
        }
    }
}
