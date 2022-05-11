using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPlatform.Verifications.DataAccess.Entities.Configuration;

internal class AuthorVerificationConfiguration : BaseEntityConfiguration<AuthorVerification>
{
    public override void Configure(EntityTypeBuilder<AuthorVerification> builder)
    {
        base.Configure(builder);

        builder.Property(av => av.PromptText)
            .HasMaxLength(500);

        builder.Property(av => av.Response)
            .HasMaxLength(500);

        builder.HasOne(av => av.VerificationStatus)
            .WithMany(vs => vs.Verifications)
            .HasForeignKey(av => av.VerificationStatusId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
