using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posts.DataAccess.Entities;

namespace Posts.DataAccess.Entities.Configuration;

internal class RatingConfiguration : BaseEntityConfiguration<Rating>
{
    public override void Configure(EntityTypeBuilder<Rating> builder)
    {
        base.Configure(builder);

        builder.HasIndex(r => new { r.PostId, r.UserId })
            .IsUnique();

        builder.HasOne(r => r.Post)
            .WithMany(p => p.Ratings)
            .HasForeignKey(r => r.PostId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
