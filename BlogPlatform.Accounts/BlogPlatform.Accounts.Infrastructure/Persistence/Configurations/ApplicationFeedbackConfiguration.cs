using BlogPlatform.Accounts.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPlatform.Accounts.Infrastructure.Persistence.Configurations;

internal class ApplicationFeedbackConfiguration : BaseEntityConfiguration<ApplicationFeedback>
{
    public override void Configure(EntityTypeBuilder<ApplicationFeedback> builder)
    {
        base.Configure(builder);

        builder.Property(af => af.ResponseText)
            .IsRequired()
            .HasMaxLength(500);
    }
}
