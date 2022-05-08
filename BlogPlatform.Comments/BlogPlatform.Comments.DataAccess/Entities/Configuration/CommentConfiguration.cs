using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPlatform.Comments.DataAccess.Entities.Configuration
{
    internal class CommentConfiguration : BaseEntityConfiguration<Comment, int>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Content)
                   .IsRequired();

            builder.Property(c => c.UpvoteCount)
                   .HasDefaultValue(0);
        }
    }
}
