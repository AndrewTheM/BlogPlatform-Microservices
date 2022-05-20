using Accounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.Infrastructure.Persistence.Configurations;

internal class AuthorApplicationConfiguration : BaseEntityConfiguration<AuthorApplication>
{
    public override void Configure(EntityTypeBuilder<AuthorApplication> builder)
    {
        base.Configure(builder);

        builder.OwnsOne(aa => aa.FullName, entity =>
        {
            entity.Property(name => name.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(name => name.MiddleName)
                .HasMaxLength(50);

            entity.Property(name => name.LastName)
                .IsRequired()
                .HasMaxLength(50);
        })
        .Navigation(aa => aa.FullName)
        .IsRequired();

        builder.Property(aa => aa.ContactEmail)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(aa => aa.Annotation)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne(aa => aa.Feedback)
            .WithOne(af => af.Application)
            .HasForeignKey<AuthorApplication>(aa => aa.FeedbackId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
